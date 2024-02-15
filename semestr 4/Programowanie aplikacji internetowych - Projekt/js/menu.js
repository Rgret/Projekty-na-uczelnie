//Point shop modal
var shop;

//shop content
var shopContent

//
var baseStats

//
var prices

//
var devModal

//
var ptn

//
var addPntValue

//
var dev = false;

//
var upgradeCount = 0;

//Start button
function Start(){
    anime({
        targets: '.bg',
        opacity: 1,
        duration: 3000,
        update: function(anim)
        {
            if(Math.round(anim.progress)>30) {window.location = 'Game.html';}
        }
    });
}

//Fade in on load
function Load(){
    anime({
    targets: '.bg',
    opacity: 0,
    duration: 7000
    });

    //generate shop prices
    if(!localStorage.levelCosts){ generateCosts(); }

    //generate base stats
    if(!localStorage.baseStats){ generateBaseStats(); }

    console.log(localStorage.points);
}

//generate starting point shop prices
function generateCosts(){
    var cost = {
        'Attack Speed': 1,              //max 2     cdSpeed
        'HP Regen Speed': 1,            //max 2     regenSpeed
        'DMG': 1,                       //          bulletDamage       
        'HP Regen': 1,                  //          regenAmmount
        'HP': 1,                        //          hp
        'Speed': 1,                     //max 2     speed
        'Bullet Speed': 1,              //max 2     bulletSpeed
        'Pick up Range': 1,             //max 2     pickRange
        'EXP Boost': 1,                 //max 2     expBoost
        'Bounce': 1,                    //max 2     bounce
        'Pierce': 1,                    //max 2     pierce
    };
    localStorage.levelCosts = JSON.stringify(cost);
}

//generate base stats
function generateBaseStats(){
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
}
var stats;var points;
//Point shop
function pointShop(){

    shop = new bootstrap.Modal(document.getElementById("pointShop"));
    shop.toggle();

    //shop content
    shopContent = document.getElementById("shopContent");

    points = document.getElementById("points");

    //prices
    prices = JSON.parse(localStorage.levelCosts);

    //Load base stats
    baseStats = JSON.parse(localStorage.baseStats);

    console.log(baseStats);
    stats = Object.keys(baseStats);

    //points.innerHTML += `Points: ${localStorage.points ? localStorage.points : 0}`;

    loadShop()
}
var key;
function loadShop(){
    prices = JSON.parse(localStorage.levelCosts);
    baseStats = JSON.parse(localStorage.baseStats);
    stats = Object.keys(baseStats);
    points.innerHTML += `Points: ${localStorage.points ? localStorage.points : 0}`;
    stats.forEach(key => {
        if(key!='Pierce' && key !='Bounce')
        {
            let btn = document.createElement('button');
            let p = document.createElement('p');
            let b = document.createElement('b');
            let id = 0;
            btn.type = 'button';
            btn.className = 'btn btn-primary';
            btn.innerHTML = key+` ${Math.round(baseStats[key]*10)/10}`;
            btn.onclick = () => {buy(key, id)};
            p.appendChild(btn);
            b.id = id;
            b.innerHTML = ` Cost: ${prices[key]}`
            p.appendChild(b);
            shopContent.appendChild(p);
            id++
        }
    });
}

function buy(stat){
    upgradeCount++;
    prices = JSON.parse(localStorage.levelCosts);
    if(localStorage.points>=prices[stat]){
        localStorage.points -= prices[stat];
        if(stat == 'HP' || stat == 'DMG' || stat == 'HP Regen' || stat == 'Pierce' || stat == 'Bounce'){
            baseStats[stat] += 1;
            prices[stat] += 1;
            points.innerHTML = ''
            shopContent.innerHTML = ''
            localStorage.baseStats = JSON.stringify(baseStats);
            localStorage.levelCosts = JSON.stringify(prices);
            loadShop()
        }else{
            baseStats[stat] += .1;
            prices[stat] += 1;
            points.innerHTML = ''
            shopContent.innerHTML = ''
            localStorage.baseStats = JSON.stringify(baseStats);
            localStorage.levelCosts = JSON.stringify(prices);
            loadShop()
        }
    }
    return stat;
}

function saveShop(){  
    localStorage.baseStats = JSON.stringify(baseStats);
    localStorage.upgradeCount = upgradeCount;
    shopContent.innerHTML = ''
    points.innerHTML = ''
    shop.toggle();
}

function showDevModal(){
    devModal = new bootstrap.Modal(document.getElementById("devModal"));
    devModal.toggle();
}

function devAddPnt(){
    addPntValue = document.getElementById("addPntValue");
    try {
        pnt = Number(addPntValue.value);
        if(pnt){ 
            addPntValue.classList.remove('is-invalid');
            localStorage.points = localStorage.points ? localStorage.points : 0
            localStorage.points = Number(localStorage.points) + pnt;
            console.log(ptn + " points added, Total: " +localStorage.points);
        }else{
            addPntValue.classList.add('is-invalid');
        };
    } catch (error) {
        console.log(error)
    }
}

function wipe(){
    localStorage.clear();
    location.reload();
}

function devClose(){
    dev = false;
    devModal.toggle();
}

document.addEventListener('keydown',(e)=>{
    if(e.code=='F2' && !dev){
        dev = true;
        showDevModal();
    }
});
