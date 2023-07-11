$(document).ready(function () {
    function updateCameraFeed() {
        $.post('/api/camera/capture');
            // .done(function (data) {
            //     // var imageUrl = 'data:image/jpeg;base64,' + btoa(String.fromCharCode.apply(null, data));
            //     // $('#cameraFeed').html('<img src="' + imageUrl + '" alt="Camera Feed" />');
            // })
            // .always(function () {
            //     setTimeout(updateCameraFeed, 100); // Delay between updates (in milliseconds)
            // });
    }

    const img = document.getElementById('camera-stream');
    fetch('http://localhost:5000/video_feed')
        .then(response => response.blob())
        .then(images => {
            let outside = URL.createObjectURL(images);
            img.src = outside;
        });
    

    updateCameraFeed();
});

//     const videoElement = document.createElement('video');
//     const canvasElement = document.getElementById('canvasElement');
//     const canvasContext = canvasElement.getContext('2d');
//
//     navigator.mediaDevices.getUserMedia({ video: true })
//     .then(function (stream) {
//     videoElement.srcObject = stream;
//     videoElement.play();
//     requestAnimationFrame(processFrame);
// })
//     .catch(function (error) {
//     console.log('Error accessing camera:', error);
// });

//     function processFrame() {
//     canvasContext.drawImage(videoElement, 0, 0, canvasElement.width, canvasElement.height);
//     requestAnimationFrame(processFrame);
// }