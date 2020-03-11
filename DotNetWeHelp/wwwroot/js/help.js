document.getElementsByTagName("BODY")[0].onresize = function() {myFunction()};
function myFunction() {
    $leftelem=document.getElementById('toolleftcontent');
    $rightelem=document.getElementById('toolrightcontent');
    var screensize = document.documentElement.clientWidth;
    if (screensize  < 767) {
        $leftelem.className='col-md-8 order-1';
        $rightelem.className='col-md-4 order-0';
    }
    else {
        $leftelem.className='col-md-8 order-0';
        $rightelem.className='col-md-4 order-1';
    }
}