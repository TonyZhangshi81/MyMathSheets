
let Room = function () {
    var param01 = 0;

	/*
	this.go = function(i, j){
		alert(i+j);
		alert(this.param01);
	}
	*/
}

Room.prototype.do = function () {

    this.param01 = 1;

    let person = new Person();
    person.name = 'abc';

    //alert(this.param01);
    //person.show(this.go);

    var url = "http://localhost/DocomoApiStub/file/001.xlsx";
    var fileName = "001.xlsx";

    //var form = $("<form></form>").attr("action", url).attr("method", "post");
    //form.append($("<input></input>").attr("type", "hidden").attr("name", "id").attr("value", fileName));
    //form.appendTo('body').submit();
}


Room.prototype.go = function (i, j) {
    //alert(i+j);
    //alert(this.room.param01);
}


Room.prototype.download = function () {
    var url = 'http://localhost/DocomoApiStub/file/001.xlsx';

    var xhr = new XMLHttpRequest();
    xhr.open('GET', url, true);
    xhr.responseType = "blob";

    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            var blob = xhr.response;
            var reader = new FileReader();
            reader.readAsDataURL(blob);
            reader.onload = function (e) {
                var a = document.createElement('a');
                a.download = 'abc.xlsx';
                a.href = e.target.result;
                $("body").append(a);
                a.click();
                $(a).remove();
            }
        } else {
            alert(xhr.statusText);
            return;
        }
    };
    xhr.send();
}

Room.prototype.download02 = function () {
    var url = 'http://localhost/DocomoApiStub/file/001.xlsx';

    var $a = $("<a/>");
    $a.attr("href", url);
    $("body").append($a);
    $a[0].click();
    $a.remove();
}
