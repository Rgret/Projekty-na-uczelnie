class Enemy {
    distance;
    hp = 10;
    damage = 1;
    size = 50;
    fill = "#F00";
    speed = 1;
    x;x2;y;y2;vx;vy;
    deltaX;deltaY;
    rad;vector;id;target;
    constructor(x, y, target, id){
        this.x = x;
        this.y = y;
        this.id = id;
        this.target = target;
        this.hp = Math.floor(this.hp*1+id/2);
    }
    draw(context){
        context.fillStyle = this.fill;
        context.fillRect(this.x, this.y, this.size, this.size)
    }
    move(){
        this.x2 = this.target.x; this.y2 = this.target.y;
        this.deltaX = this.x2 - this.x;
        this.deltaY = this.y2 - this.y;
        this.rad = Math.atan2(this.deltaY, this.deltaX);
        this.vector = new Vector(Math.cos(this.rad), Math.sin(this.rad));
        this.vector.normalizeSelf();
        this.vx = this.vector.x; this.vy = this.vector.y;
        this.distance = Math.sqrt((this.x-this.target.x)**2+(this.y-this.target.y)**2);
        if(enemiesAI){
            this.x+=this.vx * this.speed * notPaused;
            this.y+=this.vy * this.speed * notPaused;
        }
    }
    dmg(d){
        this.hp -= d;
        //console.log(this.id+" | "+this.hp)
        if(this.hp<=0){
            this.remove();
        }
    }
    remove(){
        console.log("removing id: "+this.id);
        exp[exp.length] = new Orb(
                                this.x + this.size/2,
                                this.y + this.size/2, 
                                exp.length, 
                                this.target,
                                this.id
                                );
        delete enemies[this.id];
    }
    collision(){
        var left1 = this.x;
        var right1 = this.x + this.size;
        var top1 = this.y;
        var bottom1 = this.y + this.size;
      
        var left2 = this.target.x;
        var right2 = this.target.x + this.target.size;
        var top2 = this.target.y;
        var bottom2 = this.target.y + this.target.size;
      
        // Check for collision
        if (bottom1 < top2 || top1 > bottom2 || right1 < left2 || left1 > right2) {
          // No collision
          return false;
        } else{
            this.target.damage(this.damage);
        }
    }
    tick(context){
        this.draw(context);
        if(enemiesAI)this.collision();
        this.move();
    }
}