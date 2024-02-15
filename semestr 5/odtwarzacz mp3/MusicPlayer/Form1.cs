using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.WaveFormRenderer;
using Spectrogram;
using ScottPlot;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.DataVisualization.Charting;
using ScottPlot.MarkerShapes;

namespace MusicPlayer
{
    public partial class Form1 : Form
    {
        enum Loop { noLoop, loopOnce, loopInf, loopAll }
        private Loop loop = Loop.noLoop;

        private Boolean mousePressed = false;
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        private TimeSpan duration;

        private List<string> playlist = new List<string>();
        private Dictionary<string, System.Drawing.Image> imageList = new Dictionary<string, System.Drawing.Image>();
        private Dictionary<string, TagLib.File> tagList = new Dictionary<string, TagLib.File>();

        private String currentTrack;

        private string imageFile;
        private readonly WaveFormRenderer waveFormRenderer;
        private readonly WaveFormRendererSettings standardSettings;
        private SoundCloudBlockWaveFormSettings soundCloudOrangeTransparentBlocks;

        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Multiselect = true;

            waveFormRenderer = new WaveFormRenderer();

            var topSpacerColor = Color.FromArgb(64, 83, 22, 3);
            soundCloudOrangeTransparentBlocks = new SoundCloudBlockWaveFormSettings(Color.FromArgb(196, 197, 53, 0), topSpacerColor, Color.FromArgb(196, 79, 26, 0),
                Color.FromArgb(64, 79, 79, 79))
            {
                Name = "SoundCloud Orange Transparent Blocks",
                PixelsPerPeak = 3,
                SpacerPixels = 1,
                TopSpacerGradientStartColor = topSpacerColor,
                BackgroundColor = Color.Transparent
            };
        }

        private WaveFormRendererSettings GetRendererSettings()
        {
            var settings = (WaveFormRendererSettings)soundCloudOrangeTransparentBlocks;
            settings.TopHeight = 30;
            settings.BottomHeight = 20;
            waveBox.Width = waveBox.Width + (4 - waveBox.Width % 4) + 4;
            settings.Width = waveBox.Width + (4 - waveBox.Width % 4) + 4;
            settings.DecibelScale = false;
            return settings;
        }

        private void RenderWaveform()
        {
            if (currentTrack == null) return;
            var settings = GetRendererSettings();
            if (imageFile != null)
            {
                settings.BackgroundImage = new Bitmap(imageFile);
            }
            pictureBox1.Image = null;
            Enabled = false;
            var peakProvider = new AveragePeakProvider(4);
            Task.Factory.StartNew(() => RenderThreadFunc(peakProvider, settings));
        }

        private void RenderThreadFunc(IPeakProvider peakProvider, WaveFormRendererSettings settings)
        {
            System.Drawing.Image image = null;
            try
            {
                using (var waveStream = new AudioFileReader(currentTrack))
                {
                    image = waveFormRenderer.Render(waveStream, peakProvider, settings);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            BeginInvoke((Action)(() => FinishedRender(image)));
        }

        private void FinishedRender(System.Drawing.Image image)
        {
            waveBox.Image = image;
            Enabled = true;
            //RenderSpectogram();
            Play(currentTrack);
        }

        //private void RenderSpectogram()
        //{
        //    (double[] audio, int sampleRate) = ReadMono(currentTrack);
        //    int fftSize = 16384;
        //    int targetWidthPx = spectogramBox.Width;
        //    int stepSize = audio.Length / targetWidthPx;

        //    var sg = new SpectrogramGenerator(sampleRate, fftSize, stepSize, maxFreq: 2200);
        //    sg.Add(audio);
        //    var name = Path.GetFileNameWithoutExtension(currentTrack);
        //    sg.SaveImage(name+".png", intensity: 5, dB: true);
        //    spectogramBox.Image = sg.GetBitmap();
        //}

        //(double[] audio, int sampleRate) ReadMono(string filePath, double multiplier = 16_000)
        //{
        //    var afr = new AudioFileReader(filePath);
        //    int sampleRate = afr.WaveFormat.SampleRate;
        //    int bytesPerSample = afr.WaveFormat.BitsPerSample / 8;
        //    int sampleCount = (int)(afr.Length / bytesPerSample);
        //    int channelCount = afr.WaveFormat.Channels;
        //    var audio = new List<double>(sampleCount);
        //    var buffer = new float[sampleRate * channelCount];
        //    int samplesRead = 0;
        //    while ((samplesRead = afr.Read(buffer, 0, buffer.Length)) > 0)
        //        audio.AddRange(buffer.Take(samplesRead).Select(x => x * multiplier));
        //    return (audio.ToArray(), sampleRate);
        //}

        double GetValue(int x)
        {
            double value = x * 1.0 / progressBar1.Width;
            int max = progressBar1.Maximum;
            int min = progressBar1.Minimum;
            return min + value * (max - min);
        }

        private void progressBar1_MouseDown(object sender, MouseEventArgs e)
        {
            int val = (int)GetValue(e.X);
            progressBar1.Value = val;
            progressBar1.Value = val - 1;
            progressBar1.Value = val;
            mousePressed = true;
            progressBar1.Refresh();

            audioFile.CurrentTime = TimeSpan.FromSeconds(val);
            currentTLabel.Text = audioFile.CurrentTime.ToString("mm\\:ss");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                foreach (String file in openFileDialog1.FileNames)
                {
                    try
                    {
                        var tfile = TagLib.File.Create(file);
                        
                        try
                        {
                            if (tfile.Tag.Pictures.FirstOrDefault() != null)
                            {
                                var firstPicture = tfile.Tag.Pictures.FirstOrDefault();
                                imageList.Add(file, (System.Drawing.Image)firstPicture);
                            }
                            else
                            {
                                var path = Path.GetPathRoot(currentTrack);
                                string[] files = Directory.GetFiles(path);

                                foreach (string xfile in files)
                                {
                                    var ext = Path.GetExtension(xfile);
                                    string[] jpegs = { ".jpeg", ".png", ".jpg", ".gif" };
                                    if (jpegs.Any((ej) => { return ej == ext; }))
                                    {
                                        if (Path.GetFileNameWithoutExtension(currentTrack) == Path.GetFileNameWithoutExtension(xfile))
                                        {
                                            imageList.Add(file, System.Drawing.Image.FromFile(xfile));
                                        }
                                    }
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        var fconverted = convertToWave(file);
                        playlist.Add(fconverted);
                        tagList.Add(fconverted, tfile);
                        addToPlaylist(fconverted);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                if (audioFile == null)
                {
                    changePlaying(playlist[0]);
                }
            }
        }

        private string convertToWave(string file)
        {
            var ext = Path.GetExtension(file);
            if (ext == ".wav") return file;
            else
            {
                var fName = Path.GetFileNameWithoutExtension(file);
                using (var reader = new Mp3FileReader(file))
                {
                    WaveFileWriter.CreateWaveFile(@".\\"+fName+".wav", reader);
                }
                return @".\\" + fName + ".wav";
            }
        }

        private void addToPlaylist(string track)
        {
            System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
            try
            {
                btn.Image = imageList[currentTrack];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            btn.Text = Path.GetFileNameWithoutExtension(track);
            btn.Tag = track;
            btn.Click += new EventHandler(btnClick); ;
            flowLayoutPanel1.Controls.Add(btn);
        }

        private void btnClick(Object sender, EventArgs e)
        {
            outputDevice.Stop();
            changePlaying((string)((System.Windows.Forms.Button)sender).Tag);
            timer1.Stop();
            timer2.Start();
        }

        private void changePlaying(String playing)
        {
            currentTrack = playing;
            
            outputDevice = null;
            audioFile = null;

            var ffProbe = new NReco.VideoInfo.FFProbe();
            var videoInfo = ffProbe.GetMediaInfo(currentTrack);  

            var tfile = tagList[currentTrack];
            duration = videoInfo.Duration;

            authorLabel.Text = tfile.Tag.FirstPerformer + " - " + tfile.Tag.Title;
            albumLabel.Text = tfile.Tag.Album;
            genreLabel.Text = tfile.Tag.FirstGenre;

            progressBar1.Value = 0;
            progressBar1.Maximum = duration.Seconds + duration.Minutes * 60;

            durationLabel.Text = duration.ToString("mm\\:ss");

            pictureBox1.Image = null;  
            
            try
            {
                pictureBox1.Image = imageList[currentTrack];
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            RenderWaveform();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            pause();
        }

        private void pause()
        {
            outputDevice?.Pause();
            timer1.Stop();
        }

        private void playbackStopped(object sender, EventArgs e)
        {
            if (outputDevice != null)
            {
                PlaybackState state = outputDevice.PlaybackState;

                switch (state)
                {
                    case PlaybackState.Stopped:
                        nextTrack();
                        break;
                    case PlaybackState.Paused:
                        pause();
                        break;
                    default:
                        Console.WriteLine("Unknown playback state.");
                        break;
                }
            }
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            Play(currentTrack);
            timer2.Start();
        }

        private void Play(String track)
        {
            if (outputDevice == null)
            {
                outputDevice = new WaveOutEvent();
                outputDevice.PlaybackStopped += playbackStopped;
            }
            if (audioFile == null)
            {
                audioFile = new AudioFileReader(track);
                outputDevice.Init(audioFile);
            }

            WaveChannel32 waveChannel = new WaveChannel32(audioFile);

            BufferedWaveProvider bufferedWaveProvider = new BufferedWaveProvider(waveChannel.WaveFormat);
            bufferedWaveProvider.BufferDuration = TimeSpan.FromSeconds(5);

            outputDevice.Play();
            timer1.Start();
            audioFile.Volume = 0.3f;
            currentTLabel.Text = audioFile.CurrentTime.ToString("mm\\:ss");

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.PerformStep();
            currentTLabel.Text = audioFile.CurrentTime.ToString("mm\\:ss");
        }

        private void skipButton_Click(object sender, EventArgs e)
        {
            nextTrack();
        }

        private void nextTrack()
        {
            try
            {
                switch (loop)
                {
                    case Loop.noLoop:
                        if (playlist == null) break;
                        outputDevice.Stop();
                        timer1.Stop();
                        changePlaying(playlist[playlist.IndexOf(currentTrack) + 1]);
                        timer2.Start();
                        break;
                    case Loop.loopOnce:
                        if (playlist == null) break;
                        outputDevice.Stop();
                        timer1.Stop();
                        changePlaying(currentTrack);
                        timer2.Start();
                        loop = Loop.noLoop;
                        loopOne.Checked = false;
                        break;
                    case Loop.loopInf:
                        if (playlist == null) break;
                        outputDevice.Stop();
                        timer1.Stop();
                        changePlaying(currentTrack);
                        timer2.Start();
                        break;
                    case Loop.loopAll:
                        if (playlist == null) break;
                        var lastElement = playlist[playlist.Count - 1];
                        outputDevice.Stop();
                        timer1.Stop();
                        if (lastElement == currentTrack) { 
                            changePlaying(playlist[0]);
                        } else
                        {
                            changePlaying(playlist[playlist.IndexOf(currentTrack) + 1]);
                        }
                        timer2.Start();
                        break;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Play(currentTrack);
            timer2.Stop();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            outputDevice.Stop();

            timer1.Stop();
            timer2.Stop();

            outputDevice?.Dispose();
            audioFile?.Dispose();

            playlist = null;
            imageList = null;
            tagList = null;
            imageFile = null;

            waveBox.Image = null;
            pictureBox1.Image = null;

            var path = @".\\";
            foreach (string f in Directory.EnumerateFiles(path))
            {
                var ext = Path.GetExtension(f);
                if (ext == ".wav") {
                    try
                    {
                        File.Delete(f);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }

        private void skipForwardBtn_Click(object sender, EventArgs e)
        {
            var sF = audioFile.CurrentTime.Add(new TimeSpan(0, 0, 10));
            if (sF > duration) { audioFile.CurrentTime = duration; }
            else { audioFile.CurrentTime = sF; }
            currentTLabel.Text = audioFile.CurrentTime.ToString("mm\\:ss");

            
            progressBar1.Value = (int)audioFile.CurrentTime.TotalSeconds;
            progressBar1.Value = (int)audioFile.CurrentTime.TotalSeconds - 1;
            progressBar1.Value = (int)audioFile.CurrentTime.TotalSeconds;
        }

        private void skipBackBtn_Click(object sender, EventArgs e)
        {
            var sB = audioFile.CurrentTime.Subtract(new TimeSpan(0, 0, 10));
            if (sB < TimeSpan.Zero) { audioFile.CurrentTime = TimeSpan.Zero; }
            else { audioFile.CurrentTime = sB; }

            progressBar1.Value = (int)audioFile.CurrentTime.TotalSeconds;
            currentTLabel.Text = audioFile.CurrentTime.ToString("mm\\:ss");
        }

        private void loopOne_CheckedChanged(object sender, EventArgs e)
        {
            loop = Loop.loopOnce;
            loopInf.Checked = false;
            loopAll.Checked = false;
        }

        private void loopInf_CheckedChanged(object sender, EventArgs e)
        {
            loop = Loop.loopInf;
            loopOne.Checked = false;
            loopAll.Checked = false;
        }

        private void loopAll_CheckedChanged(object sender, EventArgs e)
        {
            loop = Loop.loopAll;
            loopInf.Checked = false;
            loopOne.Checked = false;
        }
    }
}
