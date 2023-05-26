function NewHeading() {
    return '<tr>\
           <td class="span11"><input form="form-headings" type="text" name="Headings" class="span3" placeholder="Heading Name" value=""></td>\
           <td class="span1"><a class="btn btn-red btn-deleteHeading" href="#"><i class="icon-trash"></i></a></td>\
           </tr>'
}

$("#btn-newHeading").click(function () {
    $('#HeadingsTable tr:last').before(NewHeading());

    $(".btn-deleteHeading").click(function (event) {
        event.preventDefault();
        $(this).closest("tr").remove();
    });
});

$(".btn-deleteHeading").click(function (event) {
    event.preventDefault();
    $(this).closest("tr").remove();
});