$(document).ready(function () {
	
	function startTimer(duration, display) {
    var start = Date.now(),
        diff,
        minutes,
        seconds;
    function timer() {
        // get the number of seconds that have elapsed since 
        // startTimer() was called
        diff = duration - (((Date.now() - start) / 1000) | 0);

        // does the same job as parseInt truncates the float
        minutes = (diff / 60) | 0;
        seconds = (diff % 60) | 0;

        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        display.textContent = minutes + ":" + seconds;

        if (diff <= 0) {
            // add one second so that the count down starts at the full duration
            start = Date.now() + 1000;
			$(".facebookReviews").empty();
			$( ".facebookReviews" ).append( "<iframe src=\"https://www.facebook.com/plugins/post.php?href=https://www.facebook.com/michelle.northcott.18/posts/1479348875417588:0&width=500&show_text=true&appId=165309770323911&height=290\" width=\"500\" height=\"290\" style=\"border:none;overflow:hidden\" scrolling=\"no\" frameborder=\"0\" allowTransparency=\"true\"></iframe>" );
        }
    }
    
	// we don't want to wait a full second before the timer starts
    timer();
    setInterval(timer, 1000);
	}
	
	var thirtySeconds = 10;
	display = document.querySelector('#time');
    startTimer(thirtySeconds, display);

});