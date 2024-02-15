var baseStats = JSON.parse(localStorage.baseStats);

class Player {
    x = innerWidth/2-25;
    y = innerHeight/2-25;
    vxl = 0.0; vxr = 0.0; vyu = 0.0; vyd = 0.0;
    speed = baseStats['Speed'];//Speed
    bulletDamage = baseStats['DMG']; //DMG
    bulletSpeed = baseStats['Bullet Speed']; //Bullet Speed
    cd = 0;           
    cdMax = 20;
    cdSpeed = baseStats['Attack Speed']; //Attack Speed       
    inv = 0;
    invTime = 10;
    regenSpeed = baseStats['HP Regen Speed']; //HP Regen Speed
    regenAmmount = baseStats['HP Regen']; //HP Regen
    regenTime = 300;    
    regenCurrentTime = 0;
    hp = 10;            
    hpMax = baseStats['HP'];         //HP
    pickRange = baseStats['Pick up Range']; //Pick up Range
    experiance = 0;
    expToLvl = 10;
    expBoost = 1;
    lvlDif = 1.6;
    size = 50;
    fill = '#0F0'; bounce = 0; pierce = 0;
    dist;targets;targetsLength;
    expBar;HPLabel;target;godMode;
    deathModal;levelUpModal;
    constructor(){
        this.expBar = document.getElementById("expBar");
        this.HPLabel = document.getElementById("HPLabel");
        this.deathModal = new bootstrap.Modal(document.getElementById("deathModal"));
        this.levelUpModal = new bootstrap.Modal(document.getElementById("levelUpModal"));
        this.dist = Infinity;
        this.godMode = false;
    }
    draw(context) {
        context.fillStyle = this.fill;
        context.fillRect(this.x, this.y, this.size, this.size)
    }
    move() {

        //horizontal movement
        if((this.x+this.size+10)<innerWidth){this.x += this.vxr * this.speed * notPaused;}
        if((this.x-10)>0){this.x += this.vxl * this.speed * notPaused;}
        

        //vertical movement
        if((this.y-10)>0){this.y += this.vyu * this.speed * notPaused;}
        if((this.y+this.size*1.45+10<innerHeight)){this.y += this.vyd * this.speed * notPaused;}
        
        //moving HP label
        this.HPLabel.style.top = (this.y-2)+"px"; this.HPLabel.style.left = (this.x+this.size/2)-9+"px";
        this.HPLabel.innerHTML = Math.round(this.hp);
    }
    remove(){
        notPaused = false;
        this.deathModal.toggle();
        player = null;
    }
    regen(){
        if(this.regenCurrentTime<=0 && this.hp<this.hpMax){
            this.hp+=this.regenAmmount;
            this.regenCurrentTime = this.regenTime;
        }
    }
    damage(dmg){
        if(this.inv <= 0){
            this.hp-=dmg*!this.godMode;
            console.log("Got Hit! "+dmg+" DMG!")
            if(this.hp<=0){          
                console.log("Dead!");
                this.remove();
            }
            this.inv = this.invTime;
        }
    }
    levelUP(){
        this.experiance -= this.expToLvl;
        this.expToLvl = this.expToLvl*this.lvlDif;
        this.expBar.style.width = (this.experiance/this.expToLvl)*100+"%";
        reloadButtons();
        this.levelUpModal.toggle();
        notPaused = false;
    }
    devLevel(){
        this.experiance = this.expToLvl;
    }
    levelClose(){
        canvas.focus();
        notPaused = true;
        this.levelUpModal.toggle();
        if(this.experiance>=this.expToLvl){
            this.levelUP();
        }
    }
    exp(x){
        this.experiance+=x * this.expBoost;
        this.expBar.style.width = (this.experiance/this.expToLvl)*100+"%";
        if(this.experiance>=this.expToLvl && notPaused){
            this.levelUP();
        }
    }
    shoot(){
        if(this.target!=null){
            var b = new Bullet(
                this.x+this.size/2, 
                this.y+this.size/2, 
                this.target.x+this.target.size/2,  
                this.target.y+this.target.size/2, 
                bullets.length,
                this.bulletDamage,
                this.bulletSpeed,
                this.vxl>0 ? this.vxl : this.vxr,
                this.vyu>0 ? this.vyu : this.vyd,
                this.cdSpeed,
                this.bounce,
                this.pierce
                )
            bullets[bullets.length] = b;
            this.cd = this.cdMax;
        }else{
            console.log("No target!");
        }
    }
    updateTargets(targets){
        this.targets = targets;
        this.targetsLength = Object.keys(targets).length
    }
    tick(context, targets){
        this.updateTargets(targets);
        if(this.targetsLength>0 && this.cd<=0){
            for(let id in targets){
                if(targets[id].distance<this.dist){
                    this.dist = targets[id].distance;
                    this.target=targets[id];
                }
            }
            this.shoot();
        }else{
            this.dist = Infinity;
            this.target = null;
        }
        this.regen();
        this.draw(context);
        this.move();
        this.regenCurrentTime -= this.regenSpeed * notPaused;
        this.inv -= 1 * notPaused;
        this.cd -= this.cdSpeed * notPaused;
    }
};