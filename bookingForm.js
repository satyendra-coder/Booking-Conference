$(document).ready(function() {
    $('#bookingForm').on('submit', function(e) {
        e.preventDefault();

        // Validate the form
        let isValid = true;
        $('#bookingForm input, #bookingForm select').each(function() {
            if ($(this).val() === '') {
                isValid = false;
                $(this).addClass('is-invalid');
            } else {
                $(this).removeClass('is-invalid');
            }
        });

        if (isValid) {
            // Collect form data
            const formData = {
                EmployeeName: $('#employeeName').val(),
                BookingDate: $('#bookingDate').val(),
                StartTime: $('#startTime').val(),
                EndTime: $('#endTime').val(),
                RoomNumber: $('#roomNumber').val(),
                AdditionalNotes: $('#additionalNotes').val()
            };

            // Send the data to the backend
            $.ajax({
                url: '/api/bookRoom',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(formData),
                success: function(response) {
                    $('#message').html('<div class="alert alert-success">Booking successful!</div>');
                },
                error: function(xhr) {
                    $('#message').html('<div class="alert alert-danger">Error: ' + xhr.responseText + '</div>');
                }
            });
        }
    });
});