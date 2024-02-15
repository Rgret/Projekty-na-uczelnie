using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace VideoPlayer
{
    public partial class Form1 : Form
    {
        List<IWMPMedia> playlist = new List<IWMPMedia> ();
        IWMPMedia media;

        int playingIndex = 0;

        TimeSpan time = TimeSpan.Zero;

        private List<MediaInfo> mediaList;

        public class MediaInfo
        {
            public string FilePath { get; set; }
            public TagLib.File TagLibFile { get; set; }
        }

        public Form1()
        {
            InitializeComponent();

            openFileDialog1.Multiselect = true;

            axWindowsMediaPlayer1.Dock = DockStyle.Fill;
            axWindowsMediaPlayer1.uiMode = "none";

            //playlist = axWindowsMediaPlayer1.playlistCollection.newPlaylist("myplaylist");

            axWindowsMediaPlayer1.PlayStateChange += PlayerStateChanged;

            mediaList = new List<MediaInfo>();
        }

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
            progressBar1.Value = val + 1;
            progressBar1.Value = val;
            progressBar1.Refresh();
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = val;
            time = TimeSpan.FromSeconds(val);
        }

        private void AddMediaInfo(string filePath)
        {
            TagLib.File TagLibFile = TagLib.File.Create(filePath);

            mediaList.Add(new MediaInfo
            {
                FilePath = filePath,
                TagLibFile = TagLibFile,
            });
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                foreach (String file in openFileDialog1.FileNames)
                {
                    var ext = Path.GetExtension(file);
                    if (ext != ".avi") continue;
                    AddMediaInfo(file);
                    media = axWindowsMediaPlayer1.newMedia(file);
                    playlist.Add(media);
                    addToPlaylist(file);
                }
                fdurLabel.Text = "Full Duration: " + getFullDuration().ToString();
                //axWindowsMediaPlayer1.currentPlaylist = playlist;
                if (axWindowsMediaPlayer1.currentMedia != null) { return; }
                axWindowsMediaPlayer1.currentMedia = playlist[playingIndex];
                titleLabel.Text = mediaList[playingIndex].TagLibFile.Tag.Title;
            }
        }

        private TimeSpan getFullDuration()
        {
            double fullDuration = 0;
            foreach (var m in playlist)
            {
                fullDuration += m.duration;
            }

            TimeSpan t = TimeSpan.FromSeconds((int)fullDuration);
            return t;
        }

        private void addToPlaylist(string track)
        {
            Button btn = new Button();

            btn.Text = Path.GetFileNameWithoutExtension(track);
            btn.Tag = track;
            btn.Click += new EventHandler(btnClick); ;
            flowLayoutPanel1.Controls.Add(btn);
        }

        private void btnClick(Object sender, EventArgs e)
        {
            var filePath = (string)((Button)sender).Tag;
            playingIndex = mediaList.FindIndex(x => x.FilePath == filePath);
            if (playlist[playingIndex] == null) return;
            axWindowsMediaPlayer1.currentMedia = playlist[playingIndex];
        }

        private void PlayerStateChanged(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            switch (axWindowsMediaPlayer1.playState)
            {
                case WMPPlayState.wmppsPlaying:
                    progressBar1.Maximum = (int)Math.Floor(axWindowsMediaPlayer1.currentMedia.duration);
                    durationTime.Text = ((int)axWindowsMediaPlayer1.currentMedia.duration).ToString();
                    timer1.Start();
                    break;
                case WMPPlayState.wmppsReady:
                    //durationTime.Text = axWindowsMediaPlayer1.currentMedia.duration.ToString();
                    var tagLib = mediaList[playingIndex].TagLibFile;
                    titleLabel.Text = tagLib.Tag.Title;

                    time = TimeSpan.Zero;
                    progressBar1.Value = 0;
                    timer1.Start();
                    break;                
                case WMPPlayState.wmppsMediaEnded:
                    axWindowsMediaPlayer1.Ctlcontrols.next();
                    timer1.Stop();
                    if (playlist[playingIndex + 1] == null) return;
                    playingIndex++;
                    axWindowsMediaPlayer1.currentMedia = playlist[playingIndex];
                    break;
                case WMPPlayState.wmppsPaused:
                    timer1.Stop();
                    break;
                case WMPPlayState.wmppsStopped:
                    timer1.Stop();
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time = time.Add(TimeSpan.FromSeconds(1));
            currentTime.Text = time.ToString();
            //progressBar1.Value = Clamp(progressBar1.Value + 2, 0, (int)axWindowsMediaPlayer1.currentMedia.duration);
            progressBar1.Value = Clamp(progressBar1.Value + 1, 0, (int)axWindowsMediaPlayer1.currentMedia.duration); 
        }

        public static T Clamp<T>(T value, T minValue, T maxValue) where T : IComparable<T>
        {
            if (value.CompareTo(minValue) < 0)
            {
                return minValue;
            }
            else if (value.CompareTo(maxValue) > 0)
            {
                return maxValue;
            }
            else
            {
                return value;
            }
        }

        private void PlayBttn_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void PauseBttn_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void NextBttn_Click(object sender, EventArgs e)
        {
            if (mediaList.Count <= (playingIndex + 1)) return;
            playingIndex++;
            axWindowsMediaPlayer1.currentMedia = playlist[playingIndex];
        }

        private void PreviousBttn_Click(object sender, EventArgs e)
        {
            if ((playingIndex - 1) < 0) return;
            playingIndex--;
            axWindowsMediaPlayer1.currentMedia = playlist[playingIndex];
        }

        private void saveAsTxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                saveFileDialog1.Title = "Save Text File";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog1.FileName;
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        foreach (var file in mediaList)
                        {
                            sw.WriteLine(file.FilePath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void loadFromTxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    var list = File.ReadAllLines(openFileDialog1.FileName);
                    foreach (String file in list)
                    {
                        var ext = Path.GetExtension(file);
                        if (ext != ".avi") continue;
                        AddMediaInfo(file);
                        media = axWindowsMediaPlayer1.newMedia(file);
                        playlist.Add(media);
                        addToPlaylist(file);
                    }
                    fdurLabel.Text = "Full Duration: " + getFullDuration().ToString();

                    if (axWindowsMediaPlayer1.currentMedia != null) { return; }
                    axWindowsMediaPlayer1.currentMedia = playlist[playingIndex];
                    titleLabel.Text = mediaList[playingIndex].TagLibFile.Tag.Title;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
