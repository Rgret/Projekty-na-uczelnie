var directions = {
    up: ['KeyW', 'ArrowUp'],
    down: ['KeyS', 'ArrowDown'],
    left: ['KeyA', 'ArrowLeft'],
    right: ['KeyD', 'ArrowRight']
};

window.addEventListener('keydown',(e)=>{
    if(player){
        if(directions.up.includes(e.code)){
            player.vyu=-1;
        }
        if(directions.down.includes(e.code)){
            player.vyd=1;
        }
        if(directions.left.includes(e.code)){
            player.vxl=-1;
        }
        if(directions.right.includes(e.code)){
            player.vxr=1;
        }
        if(e.code=='F2'){
            if(notPaused)showDevModal();
        }
    }
});

window.addEventListener('keyup', (e)=>{
    if(player){
        if(directions.up.includes(e.code)){
            player.vyu=0;
        }
        if(directions.down.includes(e.code)){
            player.vyd=0;
        }
        if(directions.left.includes(e.code)){
            player.vxl=0;
        }
        if(directions.right.includes(e.code)){
            player.vxr=0;
        }
    }
})

window.addEventListener("mousedown", (e)=>{   
    if(player.cd<=0){
        //console.log("X:" + e.clientX + " | Y:" + e.clientY);
        var b = new Bullet(player.x+player.size/2, player.y+player.size/2, e.clientX, e.clientY, bullets.length)
        bullets[bullets.length] = b;
        player.cd = player.cdMax;
    }
});