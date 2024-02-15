//var canvas = document.getElementById("game-screen");

//Game canvas container
var screen = document.getElementById("screen")

//Game canvas
var canvas = document.createElement("canvas");
canvas.className = "object-fit-fill" 
canvas.style.position = "absolute";
var ctx = canvas.getContext('2d');

//Level Up modal
var levelUpModal = document.getElementById("levelUpModalBody");

//Dev modal
var devModal = new bootstrap.Modal(document.getElementById("devModal"));

//Pause
var notPaused = true;

//Player
var player = new Player();

//Points
var points = 0;

//Base stats local
if(localStorage.baseStats){
    var baseStats = JSON.parse(localStorage.baseStats)
}else{
    localStorage.baseStats = JSON.stringify({
        'Attack Speed': 1,              //max 2     cdSpeed
        'HP Regen Speed': 1,            //max 2     regenSpeed
        'DMG': 3,                       //          bulletDamage       
        'HP Regen': 1,                  //          regenAmmount
        'HP': 10,                       //          hp
        'Speed': 3,                     //max 2     speed
        'Bullet Speed': 5,              //max 2     bulletSpeed
        'Pick up Range': 100,           //max 2     pickRange
        'EXP Boost': 0,                 //max 2     expBoost
        'Bounce': 0,                    //max 2     bounce
        'Pierce': 0,                    //max 2     pierce
    });
    var baseStats = JSON.parse(localStorage.baseStats);
}

//upgradeCount
localStorage.upgradeCount = localStorage.upgradeCount ? localStorage.upgradeCount : 0;

//Level modifiers
var modifiers = {
    'Attack Speed': 1,              //max 2     cdSpeed
    'HP Regen Speed': 1,            //max 2     regenSpeed
    'DMG': 0,                       //          bulletDamage       
    'HP Regen': 0,                  //          regenAmmount
    'HP': 0,                        //          hp
    'Speed': 1,                     //max 2     speed
    'Bullet Speed': 1,              //max 2     bulletSpeed
    'Pick up Range': 1,             //max 2     pickRange
    'EXP Boost': 1,                 //max 2     expBoost
    'Bounce': 0,                    //max 2     bounce
    'Pierce': 0,                    //max 2     pierce
};
var stats = Object.keys(modifiers);

var loaded = false;

//Enemy spawn timer and count
let timeMax = 200;
let timeCurrent = 0;
let spawnCount = 1;
let spawnAmmount = 1;
var spawnEnemies = true;
function spawn(x, y, target, id){
    var spawner = new Spawner(x, y, target, id);
    spawners[id] = spawner;
    spawner.draw(ctx);
}

//Bullets
var bullets = [];
//Enemies
var enemies = {};
var enemiesCount = 0;
var enemiesAI = true;
//Spawners
var spawners = {};
//Exp
var exp = [];

//Random int from 0 to max
function getRandomInt(max) {
    return Math.floor(Math.random() * max);
}

//Random +/-
function plusOrMinus(){
    return getRandomInt(2)==1 ? -1:1
}

//Spawning X
function randomX(){
    if(getRandomInt(1)){
        return getRandomInt(200)+innerWidth;
    }else{
        return getRandomInt(200)*-1;
    }
}
//Spawning Y
function randomY(){
    if(getRandomInt(1)){
        return getRandomInt(200)+innerHeight;
    }else{
        return getRandomInt(200)*-1;
    }
}

//On load function
function Load(){
    anime({
    targets: '#bg',
    opacity: 0,
    duration: 3000
})

    screen.appendChild(canvas);

    ctx.canvas.width  = innerWidth*4;
    ctx.canvas.height = innerHeight*4;

    player.draw(ctx);

    loaded = true;
}

//Redraw canvas
function update() {
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    //Spawning enemies
    if(timeCurrent <= 0 && notPaused && loaded && spawnEnemies){
        for(let i = 0;i<spawnAmmount;i++){
            spawn(getRandomInt(innerWidth), getRandomInt(innerHeight), player, enemiesCount)
            enemiesCount++;
        }
        spawnAmmount = Math.floor(enemiesCount/10)+1;
        timeCurrent = timeMax;
    }
    if(notPaused)timeCurrent -=(1+(localStorage.upgradeCount/100))*spawnEnemies;

    //drawing bullets
    for(let id in bullets) {
        bullets[id].tick(ctx, enemies);
    }

    //drawing enemies
    for(let id in enemies) {
        enemies[id].tick(ctx);
    }

    //drawing exp orbs
    for(let orb in exp){
        exp[orb].tick(ctx);
    }

    //drawing exp orbs
    for(let id in spawners){
        spawners[id].tick(ctx);
    }

    //Draw player
    if(player){
        player.tick(ctx, enemies);
    }

    points = Math.floor(enemiesCount/10);

    //if(player.cd>0) player.cd-=1;
    requestAnimationFrame(update);
}
update();

//Restart game
function Restart(){
    location.reload();
}

//Focus out pause
canvas.addEventListener('focusout', (e)=>{
    console.log("Focus out.")
    notPaused = false;
})

//Focus in unpause
canvas.addEventListener('focusin', (e)=>{
    console.log("Focus in.")
    notPaused = true;
})

//Reload stat boost buttons on level up screen
function reloadButtons(dev = false) {
    while (levelUpModal.firstChild) {
        levelUpModal.removeChild(levelUpModal.lastChild);
      }
    stats.forEach(key => {
        //console.log(key);
        if(key == 'HP' || key == 'DMG' || key == 'HP Regen'){
            let btn = document.createElement('button');
            btn.type = 'button';
            btn.className = 'btn btn-primary';
            btn.innerHTML = key+"+";
            btn.onclick = () => {modifyStats(key)};
            levelUpModal.appendChild(btn);
        }else {
            if((modifiers[key]<2) || dev){
                let btn = document.createElement('button');
                btn.type = 'button';
                btn.className = 'btn btn-primary';
                btn.innerHTML = key+"+";
                btn.onclick = () => {modifyStats(key)};
                levelUpModal.appendChild(btn);
            }
        }
    });
}

//Dev Modal stuff
function showDevModal(){
    devModal.toggle();
    notPaused = false;
    statDisplay();
}
function statDisplay(){
    document.getElementById("godMode").checked = player.godMode;
    document.getElementById("spawnEnemies").checked = spawnEnemies;
    document.getElementById("enemiesAI").checked = enemiesAI;
    statModifiers = document.getElementById("statModifiersLvl");
    playerStats = document.getElementById("playerStats");
    stats.forEach(key => {
        statModifiers.innerHTML += `<p>${key} : x${Math.round(modifiers[key]*10)/10}</p>`;
    });

    // ;-;
    playerStats.innerHTML += `<p>HP : ${player.hp}</p>`;
    playerStats.innerHTML += `<p>Regen Speed : ${player.regenSpeed}</p>`;
    playerStats.innerHTML += `<p>Regen Ammount : ${player.regenAmmount}</p>`;
    
    playerStats.innerHTML += `<p>Speed : ${player.speed}</p>`;
    playerStats.innerHTML += `<p>Bullet Speed : ${player.bulletSpeed}</p>`;
    playerStats.innerHTML += `<p>Attack Speed : ${player.cdSpeed}</p>`;
    playerStats.innerHTML += `<p>Damage : ${player.bulletDamage}</p>`;
    playerStats.innerHTML += `<p>Bounce : ${player.bounce}</p>`;
    playerStats.innerHTML += `<p>Pierce : ${player.pierce}</p>`;

    playerStats.innerHTML += `<p>Exp Boost : x${player.expBoost}</p>`;
    playerStats.innerHTML += `<p>Pickup Range : ${player.pickRange}</p>`;
}
function devClose(){
    player.godMode = document.getElementById("godMode").checked;
    spawnEnemies = document.getElementById("spawnEnemies").checked;
    enemiesAI = document.getElementById("enemiesAI").checked;
    
    statModifiers.innerHTML = '';
    playerStats.innerHTML = '';
    devModal.toggle();
    notPaused = true;
    player.exp(0);
    return 1;
}
function devAddExp(){
    let addExpValue = document.getElementById("addExpValue");
    try {
        let exp = Number(addExpValue.value);
        if(exp){ 
            addExpValue.classList.remove('is-invalid');
            player.exp(exp); 
        }else{
            addExpValue.classList.add('is-invalid');
        };
    } catch (error) {
        console.log(error)
    }
}

//Modify player stats
function modifyStats(key) {
    switch (key) {
        case 'HP':
            modifiers[key] += 1;
            player.hpMax = baseStats[key] + modifiers[key];
            break;
        case 'DMG':
            modifiers[key] += 2;
            player.bulletDamage = baseStats[key] + modifiers[key];
            break;
        case 'HP Regen':
            modifiers[key] += .5;
            player.regenAmmount = baseStats[key] + modifiers[key];
            break;
        case 'Attack Speed':
            modifiers[key] += .2;
            player.cdSpeed = Math.round((baseStats[key] * modifiers[key])*10)/10;
            break;
        case 'Speed':
            modifiers[key] += .2;
            player.speed = Math.round((baseStats[key] * modifiers[key])*10)/10;
            break;
        case 'Bullet Speed':
            modifiers[key] += .2;
            player.bulletSpeed = Math.round((baseStats[key] * modifiers[key])*10)/10;
            break;
        case 'HP Regen Speed':
            modifiers[key] += .2;
            player.regenSpeed = Math.round((baseStats[key] * modifiers[key])*10)/10;
            break;
        case 'Pick up Range':
            modifiers[key] += .2;
            player.pickRange = Math.round((baseStats[key] * modifiers[key])*10)/10;
            break;
        case 'EXP Boost':
            modifiers[key] += .25;
            player.expBoost = Math.round(modifiers[key]*10)/10;
            break;
        case 'Bounce':
            modifiers[key] += 1;
            player.bounce = baseStats[key] + modifiers[key];
            break;
        case 'Pierce':
            modifiers[key] += 1;
            player.pierce = baseStats[key] + modifiers[key];
            break;
        default:
            break;
    }

    console.log(key+" = "+modifiers[key])
    player.levelClose();
    return key;
}

//dev add points
function devAddPnt(){
    let addPntValue = document.getElementById("addPntValue");
    try {
        let pnt = Number(addPntValue.value);
        if(pnt){ 
            addPntValue.classList.remove('is-invalid');
            points += pnt;
            localStorage.points = points;
            console.log(pnt + " points added, Total: " +localStorage.points);
        }else{
            addPntValue.classList.add('is-invalid');
        };
    } catch (error) {
        console.log(error)
    }
}

//Back to main menu
function Back(){
    anime({
        targets: '.bg',
        opacity: 1,
        duration: 3000,
        update: function(anim)
        {
            if(Math.round(anim.progress)>30) {window.location = 'index.html';}
        }
    })
    localStorage.points = points;
}