function NewFile() {
    return '<tr>\
                <td class="span11"><input class="input-block-level" type="text" name="Type" value="" placeholder="File Type"></td>\
                <td class="span1"><a class="btn btn-red btn-deletefiletemp" href="#"><i class="icon-trash"></i></a></td>\
            </tr>'
}

$("#btn-newfile").click(function () {
    $('#filesTable tr:last').before(NewFile());

    $(".btn-deletefiletemp").click(function (event) {
        event.preventDefault();
        $(this).closest("tr").remove();
    });
});

$(".btn-deletefile").click(function (event) {
    event.preventDefault();

    var theRow = $(this).closest("tr");

    $.post($(this).attr('href'), function (data) {

    })
    .done(function () {
        theRow.remove();
    });
});