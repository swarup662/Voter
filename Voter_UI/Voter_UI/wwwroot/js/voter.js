$('#closeVoter').click(function () {
    window.location.reload();
});
$(document).ready(function () {
    $('#tblvoter').DataTable();
});


$(document).ready(function () {
    // Initialize Bootstrap tooltips
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
});




function TagDelete(VOTER_KEY) {
    Swal.fire({
        title: 'Do you want to Delete?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            // Make a POST request to delete the record
            $.ajax({
                url: "/VoterUI/DeleteVote",
                method: 'POST',
                data: {
                    id: VOTER_KEY
                },
                success: function (data) {
                    console.log("Delete response received:", data);
                    if (data.msg === "success") {
                        Swal.fire("Done", "Record Deleted Successfully!", "success");
                    }
                    else {
                        Swal.fire("Oops!!!", "Please Contact Admin", "error");
                    }
                    // Reload the page after deletion
                    window.location.reload();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.error("Error occurred while deleting:", textStatus, errorThrown);
                    Swal.fire("Oops!!!", "An error occurred while deleting the record.", "error");
                }
            });
        } else if (result.isDenied) {
            Swal.fire('Welcome', '', 'info');
        }
    });
}




//function EditVote(id) {

//    debugger;


//    $.getJSON("/VoterUI/EditVote/" + id,

//        function (d) {
//            // Set values for text fields
//            console.log(d);
//            debugger;
//            $("#VOTER_KEY").val(vOTER_KEY);
//            $("#NAME").val(d.nAME);
//            $("#AGE").val(d.aGE);
//            $("input[name='GENDER_ID']").prop("checked", false);
//            $("input[name='GENDER_ID'][value='" + d.gendeR_ID + "']").prop("checked", true);
//            $("#STATE_ID").val(d.statE_ID);
//            $("#VOTERCARD_NO").val(d.votercarD_NO);



//            $("#VoterAPIModal").modal('show');
//            document.getElementById("staticBackdropLabel").innerHTML = "Voter Update Form:: [Edit]";
//        });
//}


//function EditVote(id) {
//    $.getJSON("/VoterUI/EditVote/" + id, function (d) {
//         Log the data to confirm it is correct
//        console.log(d);

//        $("#VOTER_KEY").val(d.voter_KEY); 
//        $("#NAME").val(d.name);
//        $("#AGE").val(d.age); 
//         Set value for Gender radio buttons
//        $("input[name='GENDER_ID']").prop("checked", false);
//        $("input[name='GENDER_ID'][value='" + d.gender_ID + "']").prop("checked", true);
//        $("#STATE_ID").val(d.state_ID);
//        $("#VOTERCARD_NO").val(d.votercard_NO);

//         Show the correct modal
//        $("#VoterModal").modal('show');
//        document.getElementById("staticBackdropLabel").innerHTML = "Voter Update Form:: [Edit]";
//    });
//}


function EditVote(id) {
    console.log("EditVote called with VOTER_KEY:", id);
    $.getJSON("/VoterUI/EditVote/" + id, function (d) {
        console.log("Data received:", d); // Log the data received

        // Check if data is correctly fetched
        if (d) {
            $("#VOTER_KEY").val(d.voteR_KEY);
            $("#NAME").val(d.name);
            $("#AGE").val(d.age);
            // Set value for Gender radio buttons
            $("input[name='GENDER_ID']").prop("checked", false);
            $("input[name='GENDER_ID'][value='" + d.gendeR_ID + "']").prop("checked", true);
            $("#STATE_ID").val(d.statE_ID);
            $("#VOTERCARD_NO").val(d.votercarD_NO);
           


            // Display the modal
            $("#VoterModal").modal('show');

            $("#staticBackdropLabel").html('Voter Update Form:: [Edit]');
        } else {
            console.error("No data received for editing.");
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        console.error("Failed to fetch data for editing:", textStatus, errorThrown);
    });
}
