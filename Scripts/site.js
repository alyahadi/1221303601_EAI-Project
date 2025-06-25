$(document).ready(function () {
    console.log("site.js is running!"); // <-- ADD THIS LINE

    $('#resetFilterBtn').on('click', function () {
        console.log("Reset button clicked!"); // <-- ADD THIS LINE to test the click
        $('#categoryFilter').val('');
        $(this).closest('form').submit();
    });
});