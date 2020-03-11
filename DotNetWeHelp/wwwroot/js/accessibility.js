function setCookie(name,value,days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days*24*60*60*1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "")  + expires + "; path=/";
}

function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for(var i=0;i < ca.length;i++) {
        var c = ca[i];
        while (c.charAt(0)==' ') c = c.substring(1,c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
    }
    return null;
}

function eraseCookie(name) {
    document.cookie = name+'=; Max-Age=-99999999;';
}

function CookieFromBtn(){
    //if the first statement returns null, it means it has not been set
    if (getCookie('largerText')==null){
        //set a cookie to result to yes
        setCookie('largerText',true,90);
        //run the checkforcookieonload() to readjust css styles
        checkForCookieOnLoad();

    } else if (getCookie('largerText')!=null) {
        //erases the current cookie
        eraseCookie('largerText');
        //run the checkforcookieonload() to readjust css styles
        checkForCookieOnLoad();
    }

}

function checkForCookieOnLoad(){
    var elements1 = document.getElementsByClassName('nav-item');
    var elements2 = document.getElementsByClassName('col-md-4');
    var elements3 = document.getElementsByClassName('col-md-8');
    var elements4 = document.getElementsByClassName('col-sm');
    var totalLength = elements1.length + elements2.length +elements3.length;
    // console.log(totalLength);
    // console.log(elements1);
    // console.log(elements2);
    // console.log(elements3);
    // console.log(elements4);

    if (getCookie('largerText')==null){
        console.log('no cookie found for text');
        document.body.style.fontSize = "medium";

        //create the logic to loop over and add styles below
        for(var i =0; i < totalLength; i++){

            if (elements1.length!=null){
                if (i < elements1.length){
                    console.log(elements1.item(i).style.fontSize = 'large');
                }
            }

            if (elements2.length!=null){
                if (i < elements2.length){
                    console.log(elements2.item(i).style.fontSize = 'medium');

                }
            }

            if (elements3.length!=null){
                if (i < elements3.length){
                    console.log(elements3.item(i).style.fontSize = 'medium');

                }
            }

        }


    }else {
        console.log('cookie found for text');
        document.body.style.fontSize = "x-large";

        // console.log(elements1);
        // console.log(elements2);
        // console.log(elements3);

        //create the logic to loop over and add styles below
        for(var i =0; i < totalLength; i++){

            if (elements1.length!=null){
                if (i < elements1.length){
                    console.log(elements1.item(i).style.fontSize = 'x-large');
                }
            }

            if (elements2.length!=null){
                if (i < elements2.length){
                    console.log(elements2.item(i).style.fontSize = 'x-large');

                }
            }

            if (elements3.length!=null){
                if (i < elements3.length){
                    console.log(elements3.item(i).style.fontSize = 'x-large');

                }
            }

        }

    }
}

/**
 * This function will load on every load of the headerfooter.blade file for persistance
 */
checkForCookieOnLoad();




