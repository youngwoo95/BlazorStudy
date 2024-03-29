﻿/*
function btnClick() {
    var temp = document.getElementById("qrcode");

    // 이전 QR 코드 제거
    temp.innerHTML = "";

    var qrcode = new QRCode(temp,
        {
            width: 100,
            height: 100
        });

    makeCode();

    function makeCode() {
        var elText = document.getElementById("text");
        if (!elText.value) {
            alert("Input a text");
            elText.focus();
            return;
        }
        qrcode.makeCode(elText.value);
    }
}
*/

function qrcreate(strValue) {
    var temp = document.getElementById("qrcodes");

    // 이전 QR 코드 제거
    temp.innerHTML = "";

    var qrcode = new QRCode(temp,
        {
            width: 100,
            height: 100
        });
    // QR 생성
    qrcode.makeCode(strValue);
    
    temp.addEventListener('click', function () {
        var temp1 = temp.querySelector('img');
        var temp2= temp1.src;

        /////////// blob 변환 /////////////
        var imgData = atob(temp2.split(",")[1]);
        var len = imgData.length;
        var buf = new ArrayBuffer(len); // 비트를 담을 버퍼를 만든다.
        var view = new Uint8Array(buf); // 버퍼를 8bit Unsigned Int로 담는다.
        var blob, i;

        for (i = 0; i < len; i++) {
            view[i] = imgData.charCodeAt(i) & 0xff; // 비트 마스킹을 통해 msb를 보호한다.
        }
        // Blob 객체를 image/png 타입으로 생성한다. (application/octet-stream도 가능)
        blob = new Blob([view], { type: "image/png" });
        console.log(blob);
        ////////////////////////


        const url = window.URL.createObjectURL(blob); // 객체 URL 생성 (페이지 내에서 Blob에 담긴 바이너리 데이터를 참조할 수 있는 URL을 생성한다.)

        const anchor = document.createElement("a"); // 가상 anchor 태그 생성
        anchor.href = url; // anchor 태그에 생성한 URL을 연결
        anchor.download = "test.png"; // 다운로드 파일 이름
        anchor.click(); // 클릭이벤트 발생
        
        
        window.URL.revokeObjectURL(blob) // URL을 해제해줘야함 - 가비지컬렉터
        
        //window.open(URL.createObjectURL(blob)); // 새 브라우저에서 열기

    })    
};

// 화면 PDF 변환
function btnPrint() {
    var divContents = document.getElementById("text").innerHTML;
    var printWindow = window.open('', '', 'height=400,width=800');
    printWindow.document.write('<html><head><title>제목이 들어갈 자리</title>');
    printWindow.document.write('</head><body >');
    printWindow.document.write(divContents);
    printWindow.document.write('</body></html>');
    setTimeout(function () {
        printWindow.print();
        printWindow.document.close();
    }, 250);
}



// 엑셀 파일 다운로드
function saveAsFile(filename, bytesBase64) {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    document.body.removeChild(link);
}

// 마지막 행에 추가
function AddClick() {
    var tableData = document.getElementById("testtable");
    var row = tableData.insertRow(tableData.rows.length);

    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);

    cell1.innerHTML = "<td><input type='checkbox' name='chkbox'></td>";
    cell2.innerHTML = `<td><p> 여기엔 내용이 들어갑니다. </p></td>`;
    //cell2.innerHTML = `<td><p>` + i + `</p></td>`;
}

// 마지막 행 삭제
function DeleteClick() {
    var tableData = document.getElementById("testtable");

    if (tableData.rows.length < 1) return;

    tableData.deleteRow(tableData.rows.length - 1); // 맨 아래 행 삭제
}

// 선택된 행 삭제
function SelectDeleteClick() {
    var tableData = document.getElementById('testtable');
    for (var i = 0; i < tableData.rows.length; i++) {
        var checkbox = tableData.rows[i].cells[0].childNodes[0].checked;

        if (checkbox) {
            tableData.deleteRow(i);
            //var text = tableData.rows[i].cells[1];
            //console.log(tableData.rows[i].cells[1].textContent);
            i--;
        }

    }
}

// 선택된 행 출력
function SelectPrintClick() {
    var tableData = document.getElementById('testtable');
    for (var i = 0; i < tableData.rows.length; i++) {
        var checkbox = tableData.rows[i].cells[0].childNodes[0].checked;

        if (checkbox) {
            var text = tableData.rows[i].cells[1];
            console.log(tableData.rows[i].cells[1].textContent);
        }

    }
}