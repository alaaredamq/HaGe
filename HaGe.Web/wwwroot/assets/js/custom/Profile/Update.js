const initUpdateProfile = () => {
    $(document).on('click', '#kt_account_profile_details_submit', function (e) {
        e.preventDefault();
        // let data = form.serialize();
        let data = new FormData();
        data.append('FirstName', $('#FirstName').val());
        data.append('LastName', $('#LastName').val());
        data.append('Email', $('#Email').val());
        // data.append('Country', $('#Country').val());
        
        // let fileInput = document.querySelector('#ProfilePhoto');
        // if (fileInput.files.length > 0) {
        //     data.append('ProfilePhoto', fileInput.files[0]);
        // }
        
        let url = "/Auth/UpdateProfileDetails";
        let method = "POST";
        $.ajax({
            url: url,
            method: method,
            data: data,
            processType: false,
            contentType: false,
            success: function (response) {
                if (response.status == 200) {
                    // Swal.fire({
                    //     icon: 'success',
                    //     title: 'Profile Updated Successfully',
                    //     showConfirmButton: false,
                    //     timer: 1500
                    // }).done(function () {
                    //     setTimeout(function () {
                    //         window.location.href = "/profile";
                    //     }, 1500);
                    // }); 
                } else {
                    // Swal.fire({
                    //     icon: 'error',
                    //     title: 'Oops...',
                    //     text: response.message,
                    // })
                }
            },
            error: function (response) {
                alert(response.message);
            }
        })
    });
}

$(document).ready(function(){
    initUpdateProfile();
})