function viewGraph(){
$('.column').css('height','0');
$('#MyGraph tr').each(function(index) {
    var ha = $(this).children('td').eq(1).text();
    var val = ha.replace('%', '')
    ha = ha * 3.33
    ha = ha + '%'
       $('#col' + index).animate({ height: ha }, 1500).html("<div >" + val + "</div>");
});
}
$(document).ready(function(){
    viewGraph();
    viewGraph2();
});

function viewGraph2() {
    $('.column').css('height', '0');
    $('#MyGraph1 tr').each(function (index) {
        var ha = $(this).children('td').eq(1).text();
        var val = ha.replace('%', '')
        ha = ha /150
        ha = ha + '%'
        $('#colum' + index).animate({ height: ha }, 1500).html("<div >" + val + "</div>");
    });
}