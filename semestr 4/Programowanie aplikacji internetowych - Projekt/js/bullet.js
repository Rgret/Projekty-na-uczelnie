class Bullet {
    damage = 3;
    size = 10;
    fill = "#FF0";
    speed = 5;
    x1;x2;y1;y2;id;vx;vy;
    vxm;vym;spread;
    deltaX;deltaY;
    rad;vector;id;
    bounce;pierce;
    pierced = [];
    constructor(x1, y1, x2, y2, id, damage, speed, vxm, vym, spread, bounce, pierce){
        this.x1 = x1;
        this.y1 = y1;
        this.vxm = vxm;
        this.vym = vym;
        this.x2 = x2 - this.size/2;
        this.y2 = y2 - this.size/2;
        this.id = id;
        this.damage = damage;
        this.speed = speed;
        this.spread = spread;
        this.bounce = bounce;
        this.pierce = pierce;
        this.deltaX = this.x2 - this.x1;
        this.deltaY = this.y2 - this.y1;
        this.rad = Math.atan2(this.deltaY, this.deltaX);
        this.rad += (getRandomInt(spread*3)* (Math.PI/180))*plusOrMinus();
        this.vector = new Vector(Math.cos(this.rad), Math.sin(this.rad));
        this.vector.normalizeSelf();
        this.vx = this.vector.x; this.vy = this.vector.y;
    }
    draw(context){
        context.save();
        context.fillStyle = this.fill;
        context.fillRect(this.x1, this.y1, this.size, this.size)
        context.restore();
    }
    remove(){
        //console.log("removing id: "+this.id);
        delete bullets[this.id];
    }
    move(){  
        this.x1 += this.vector.x * this.speed * notPaused;
        this.y1 += this.vector.y * this.speed * notPaused;
    }
    collision(e){
        if(this.x1 > innerWidth || this.y1 > innerHeight){
            if(!this.bounce){
                this.remove();
            }else{
                this.vector.x = -this.vector.x;
                this.vector.y = -this.vector.y;
                this.bounce -= 1;
            }
        }
        for(let id in e){
            if(this.x1+this.size/2>e[id].x && this.x1+this.size/2<e[id].x+e[id].size
            && this.y1+this.size/2>e[id].y && this.y1+this.size/2<e[id].y+e[id].size){
                if(this.bounce){
                    e[id].dmg(this.damage);
                    this.vector.x = -this.vector.x;
                    this.vector.y = -this.vector.y;
                    this.bounce--;
                }else if(this.pierce>0 && !this.pierced.includes(id)){
                    console.log("pierced!");
                    this.pierced.unshift(id)
                    e[id].dmg(this.damage);
                    this.pierce--;
                }else if(!this.pierced.includes(id)){
                    e[id].dmg(this.damage);
                    this.remove()
                }
            }
        }
    }
    tick(context, targets){
        this.draw(context);
        this.move();
        this.collision(targets);
    }
}
