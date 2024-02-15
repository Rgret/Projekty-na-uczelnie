class Spawner {
    timer = 0;
    x;y;target;id;
    fill = "rgba(255, 0, 0, 0)"
    constructor(x, y, target,id){
        this.x = x;
        this.y = y;
        this.target = target;
        this.id = id;
    }
    draw(context) {
        context.fillStyle = this.fill;
        context.fillRect(this.x, this.y, 50, 50)
    }
    remove(){
        delete spawners[this.id];
        enemies[this.id] = new Enemy(this.x, this.y, this.target, this.id);
    }
    tick(context) {
        this.fill = `rgba(255, 0, 0, ${(this.timer)/100})`
        if(this.timer>=90){
            this.remove();
        }
        this.draw(context)
        this.timer +=1*notPaused;
    }
}