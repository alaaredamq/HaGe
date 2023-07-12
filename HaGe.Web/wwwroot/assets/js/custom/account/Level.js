var triesTotal = 0;
$(document).ready(function(){
    var triesElements = $(".tries");
    triesElements.each(function(){
        var tries = $(this).text();
        var triesInt = parseInt(tries);
        triesTotal += triesInt;
    })
    $("#tries_total").html(triesTotal);
})