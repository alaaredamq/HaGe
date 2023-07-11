﻿let video;

$(document).ready(() => {
    video = document.getElementById('camera_footage');
    // startVideo();
    // renderVideo();
    LaunchPython();
})

const startVideo = () => {
    navigator.mediaDevices.getUserMedia({video: true})
        .then((stream) => {
            video.srcObject = stream;
        })
        .catch((err) => {
            console.error(`Error: ${err}`);
        });
}

const renderVideo = () => {
    video.addEventListener('loadedmetadata', () => {
        video.play();
    });
}

const LaunchPython = () => {
    $("#launch_python").on("click", function () {
        var link = "http://localhost:5000/trigger";
        if ($("#LevelName").val().length > 0){
            link += $("#LevelName").val();
        } else {
            link += "Easy";
        }
        // if ($("#Level").val() == "1") {
        //     link = "http://localhost:5000/triggerEasy";
        // } else if($("#Level").val() == "2") {
        //     link = "http://localhost:5000/triggerMedium";
        // } else if($("#Level").val() == "3") {
        //     link = "http://localhost:5000/triggerHard";
        // }
        console.log(link);
        // var formData = new FormData();
        // formData.append('path', $("#Level").val())
        $.ajax({
            url: link,
            processType: false,
            contentType: false,
            method: "POST",
            // data: formData,
            success: function (data) {
                if (parseFloat(data.stars) > 0) {
                    nextLevel();
                }
                // updateStats(data);
                // if (parseFloat(data.stars) < 2) {
                //     Swal.fire({
                //         title: "Opps!",
                //         text: "Try again and get a higher score to unlock next level!",
                //         icon: "error",
                //         buttonsStyling: false,
                //         confirmButtonText: "Ok, got it!",
                //         customClass: {
                //             confirmButton: "btn btn-primary"
                //         }
                //     })
                // } else {
                //     Swal.fire({
                //         title: "Good job!",
                //         html: "You have Passed this level!<br>Here are your stats:<br><br>Words Spoken: " + data.sentence_written + "<br>Stars: " + data.stars+ "<br>Score: " + data.score + "<br>Time: " + parseInt(data.session_length) + " seconds",
                //         icon: "success",
                //         buttonsStyling: false,
                //         confirmButtonText: "Ok, got it!",
                //         customClass: {
                //             confirmButton: "btn btn-primary"
                //         }
                //     }).then(function () {
                //         updateStats(data);
                //         nextLevel();
                //     })
                // }
                console.log(data)
            },
            error: function (data) {
                console.error(data)
            }
        })
    });
}

const nextLevel = () => {
    var formData = new FormData();
    formData.append('LockOrder', $("#LockOrder").val())
    $.ajax({
        url: "/UnlockNextLevel",
        method: "POST",
        data: formData,
        processData: false, 
        contentType: false,
        // data: {LockOrder: $("#LockOrder").val()},
        success: function (data) {
            if (data.success){
                if (data.level == parseInt($("#LockOrder").val()) + 1) {
                    Swal.fire({
                        title: "Congratulations!",
                        text: "You have unlocked the next level!",
                        icon: "success",
                        timer: 2000,
                        showConfirmButton: false
                    })
                }
            } else {
                Swal.fire({
                    title: "Opps!",
                    text: "Something went wrong, please try again later!",
                    icon: "error",
                    timer: 2000,
                    showConfirmButton: false
                })
            }
        },
        error: function (data, status, error) {
            // swal with timer and no buttons
            Swal.fire({
                title: "Opps!",
                text: "Try again and get a higher score to unlock next level!",
                icon: "error",
                timer: 2000,
                showConfirmButton: false
            })
        }
    })
}

const updateStats = (data) => {
    let formData = new FormData();
    var list = []
    // data.sign_display_count.each((element, index) => {
    formData.append('stats', data.sign_display_count);
    for (var key in data.sign_display_count){
        var value = new WordOccurence(key, data.sign_display_count[key]);
        list.push(value)
    }
    
    formData.append('stats', JSON.stringify(list));
    
    // });
    
    $.ajax({
        url: "/updateStats",
        processType: false,
        contentType: false,
        method: "POST",
        data: formData,
        success: function (data) {
            console.log(data)
        },
        error: function (data, status, error) {
            // swal with timer and no buttons
            Swal.fire({
                title: "Opps!",
                text: "Something went wrong, please try again later!",
                icon: "error",
                timer: 2000,
                showConfirmButton: false
            })
        }
    })
}

class WordOccurence{
    constructor(word, count) {
        this.Word = word;
        this.Count = count;
    }
}