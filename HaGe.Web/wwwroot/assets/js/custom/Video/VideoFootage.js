let video;

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
        // var formData = new FormData();
        // formData.append('path', $("#Level").val())
        $.ajax({
            url: "http://localhost:5000/trigger",
            processType: false,
            contentType: false,
            method: "POST",
            // data: formData,
            success: function (data) {
                if (parseFloat(data.stars) < 2) {
                    Swal.fire({
                        title: "Opps!",
                        text: "Try again and get a higher score to unlock next level!",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn btn-primary"
                        }
                    })
                } else {
                    nextLevel();
                }
                console.log(data)
            },
            error: function (data) {
                console.error(data)
            }
        })
    });
}

const nextLevel = () => {
    $.ajax({
        url: "/UnlockNextLevel",
        processType: false,
        contentType: false,
        method: "POST",
        success: function (data) {
            console.log(data)
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