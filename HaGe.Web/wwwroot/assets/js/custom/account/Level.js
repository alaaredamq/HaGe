var triesTotal = 0;
var HighScoreTotal = 0;
$(document).ready(function(){
    var triesElements = $(".tries");
    triesElements.each(function(){
        var tries = $(this).text();
        var triesInt = parseInt(tries);
        triesTotal += triesInt;
    })
    $("#tries_total").html(triesTotal);
    
    var percentageElements = $(".highestScore");
    percentageElements.each(function(){
        var perc = $(this).text().replace("%", "");
        var percInt = parseInt(perc);
        HighScoreTotal = HighScoreTotal >= percInt ? HighScoreTotal : percInt;
    })
    $("#HighScore").html(HighScoreTotal);
    
})