class Orb {
    value = 2;
    speed = 30;
    size = 10;
    fill = "#00F";
    x;y;x2;y2;vx;vy;id;
    deltaX;deltaY;vector;
    distance;target;
    constructor(x, y, id, target, valueBoost) {
        this.x = x;
        this.y = y;
        this.id = id;
        this.target = target;
        this.value *= 1+valueBoost/10;
        this.x2 = target.x+target.size/2;
        this.y2 = target.y+target.size/2;
        this.distance = Math.sqrt((this.x-this.x2)^2+(this.y-this.y2)^2);
    }
    draw(context){
        context.fillStyle = this.fill;
        context.fillRect(this.x - this.size/2, this.y - this.size/2, this.size, this.size)
    }
    move(){
        this.x2 = this.target.x+this.target.size/2;
        this.y2 = this.target.y+this.target.size/2;
        this.deltaX = this.x2 - this.x;
        this.deltaY = this.y2 - this.y;
        this.rad = Math.atan2(this.deltaY, this.deltaX);
        this.vector = new Vector(Math.cos(this.rad), Math.sin(this.rad));
        this.vector.normalizeSelf();
        this.vx = this.vector.x; this.vy = this.vector.y;
        this.distance = Math.sqrt((this.x-this.x2)**2+(this.y-this.y2)**2);
        if(this.distance <= this.target.pickRange){
            this.x+=this.vx * this.speed * notPaused;
            this.y+=this.vy * this.speed * notPaused;
        }
    }
    remove(){
        this.target.exp(this.value);
        delete exp[this.id];
    }
    tick(context){
        this.draw(context);
        this.move();
        if(this.distance<this.target.size/2){
            this.remove();
        }
    }
}