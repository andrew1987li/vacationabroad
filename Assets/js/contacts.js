setInterval(showcurrenttime, 1000);



function showcurrenttime() {
    var dt = new Date();
    $('#curtime').text(dt.getHours()+ ":" + dt.getMinutes() + ":" + dt.getSeconds());
    
}