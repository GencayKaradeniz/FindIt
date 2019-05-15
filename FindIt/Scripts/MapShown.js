
$(document).ready(function () {
    var cookie3, cookieArray3 = [], cookies3 = [];
    var cookie2, cookieArray2 = [], cookies2 = [];
    var cookie1, cookieArray1 = [], cookies1 = [];

    cookie3 = $.cookie("mapRouteCoordinates3");
    cookie2 = $.cookie("mapRouteCoordinates2");
    cookie1 = $.cookie("mapRouteCoordinates1");
    var id = parseInt(location.pathname.substring(location.pathname.lastIndexOf("/") + 1));

    if (cookie3 != null) {
        cookie3 = cookie3.split('&');
        cookie2 = cookie2.split('&');
        cookie1 = cookie1.split('&');

        for (i = 0; i < cookie3.length; i++) {
            cookies3 = cookie3[i].split('=');
            cookieArray3[i] = cookies3[1];

            cookies2 = cookie2[i].split('=');
            cookieArray2[i] = cookies2[1];

            cookies1 = cookie1[i].split('=');
            cookieArray1[i] = cookies1[1];
        }

        if (cookieArray1[0] == id || cookieArray2[0] == id || cookieArray3[0] == id) {
            $('#addRoute').removeClass('btn-success');
            $('#addRoute').addClass('btn-danger');
            $('#addRoute').val('Rotadan çıkart');

            drawRouteWithCookies();
        }
        else {
            $('#addRoute').attr("disabled", true);
        }
    }
    else if (cookie2 != null) {
        cookie2 = cookie2.split('&');
        cookie1 = cookie1.split('&');

        for (i = 0; i < cookie2.length; i++) {
            cookies2 = cookie2[i].split('=');
            cookieArray2[i] = cookies2[1];

            cookies1 = cookie1[i].split('=');
            cookieArray1[i] = cookies1[1];
        }

        if (cookieArray1[0] == id || cookieArray2[0] == id) {
            $('#addRoute').removeClass('btn-success');
            $('#addRoute').addClass('btn-danger');
            $('#addRoute').val('Rotadan çıkart');

            drawRouteWithCookies();
        }
    }
    else if (cookie1 != null) {
        cookie1 = cookie1.split('&');

        for (i = 0; i < cookie1.length; i++) {
            cookies1 = cookie1[i].split('=');
            cookieArray1[i] = cookies1[1];
        }

        if (cookieArray1[0] == id) {
            $('#addRoute').removeClass('btn-success');
            $('#addRoute').addClass('btn-danger');
            $('#addRoute').val('Rotadan çıkart');
            drawRouteWithCookies();
        }
    }
});

function drawRouteWithCookies() {
    var ctx = null;
    var finditScreenX = 2;
    var finditScreenY = 14;
    var shopMap = drawMap(finditScreenX, finditScreenY);
    var colorLine, colorTarget;

    var cookieManhattanArray = new Array(3);
    var cookie3, cookieArray3 = [], cookies3 = [];
    var cookie2, cookieArray2 = [], cookies2 = [];
    var cookie1, cookieArray1 = [], cookies1 = [];

    cookie3 = $.cookie("mapRouteCoordinates3");
    cookie2 = $.cookie("mapRouteCoordinates2");
    cookie1 = $.cookie("mapRouteCoordinates1");

    if ($.cookie("mapRouteCoordinates3") != null) {
        cookie3 = cookie3.split('&');
        cookie2 = cookie2.split('&');
        cookie1 = cookie1.split('&');

        for (i = 0; i < cookie3.length; i++) {
            cookies3 = cookie3[i].split('=');
            cookieArray3[i] = cookies3[1];

            cookies2 = cookie2[i].split('=');
            cookieArray2[i] = cookies2[1];

            cookies1 = cookie1[i].split('=');
            cookieArray1[i] = cookies1[1];
        }

        cookieManhattanArray[0] = [Math.abs(finditScreenX - Math.floor((parseInt(cookieArray1[1]) + parseInt(cookieArray1[2])) / 2)) + Math.abs(finditScreenY - parseInt(cookieArray1[3])), cookieArray1];
        cookieManhattanArray[1] = [Math.abs(finditScreenX - Math.floor((parseInt(cookieArray2[1]) + parseInt(cookieArray2[2])) / 2)) + Math.abs(finditScreenY - parseInt(cookieArray2[3])), cookieArray2];
        cookieManhattanArray[2] = [Math.abs(finditScreenX - Math.floor((parseInt(cookieArray3[1]) + parseInt(cookieArray3[2])) / 2)) + Math.abs(finditScreenY - parseInt(cookieArray3[3])), cookieArray3];
        cookieManhattanArray.sort(function (a, b) { return a[0] - b[0] });

        colorLine = '#d53f4e';
        colorTarget = '#eced4d';
        ManhattanArray = calculateManhattanArray(10, shopMap, finditScreenX, finditScreenY, Math.floor((parseInt(cookieManhattanArray[0][1][1]) + parseInt(cookieManhattanArray[0][1][2])) / 2), parseInt(cookieManhattanArray[0][1][3]));
        drawRoute(ManhattanArray, shopMap, colorLine, colorTarget);

        colorLine = '#83ffc8';
        colorTarget = '#ff83dc';
        ManhattanArray = calculateManhattanArray(10, shopMap, ManhattanArray[ManhattanArray.length - 1][0], ManhattanArray[ManhattanArray.length - 1][1], Math.floor((parseInt(cookieManhattanArray[1][1][1]) + parseInt(cookieManhattanArray[1][1][2])) / 2), parseInt(cookieManhattanArray[1][1][3]));
        drawRoute(ManhattanArray, shopMap, colorLine, colorTarget);

        colorLine = '#2a1b26';
        colorTarget = '#af83dc';
        ManhattanArray = calculateManhattanArray(10, shopMap, ManhattanArray[ManhattanArray.length - 1][0], ManhattanArray[ManhattanArray.length - 1][1], Math.floor((parseInt(cookieManhattanArray[2][1][1]) + parseInt(cookieManhattanArray[2][1][2])) / 2), parseInt(cookieManhattanArray[2][1][3]));
        drawRoute(ManhattanArray, shopMap, colorLine, colorTarget);

    }
    else if ($.cookie("mapRouteCoordinates2") != null) {

        cookie2 = cookie2.split('&');
        cookie1 = cookie1.split('&');

        for (i = 0; i < cookie2.length; i++) {
            cookies2 = cookie2[i].split('=');
            cookieArray2[i] = cookies2[1];

            cookies1 = cookie1[i].split('=');
            cookieArray1[i] = cookies1[1];
        }
        var ManhattanArray;
        cookieManhattanArray[0] = [Math.abs(finditScreenX - Math.floor((parseInt(cookieArray1[1]) + parseInt(cookieArray1[2])) / 2)) + Math.abs(finditScreenY - parseInt(cookieArray1[3]))];
        cookieManhattanArray[1] = [Math.abs(finditScreenX - Math.floor((parseInt(cookieArray2[1]) + parseInt(cookieArray2[2])) / 2)) + Math.abs(finditScreenY - parseInt(cookieArray2[3]))];
        if (cookieManhattanArray[0] < cookieManhattanArray[1]) {
            colorLine = '#d53f4e';
            colorTarget = '#eced4d';
            ManhattanArray = calculateManhattanArray(10, shopMap, finditScreenX, finditScreenY, Math.floor((parseInt(cookieArray1[1]) + parseInt(cookieArray1[2])) / 2), parseInt(cookieArray1[3]));
            drawRoute(ManhattanArray, shopMap, colorLine, colorTarget);
            colorLine = '#83ffc8';
            colorTarget = '#ff83dc';
            ManhattanArray = calculateManhattanArray(10, shopMap, ManhattanArray[ManhattanArray.length - 1][0], ManhattanArray[ManhattanArray.length - 1][1], Math.floor((parseInt(cookieArray2[1]) + parseInt(cookieArray2[2])) / 2), parseInt(cookieArray2[3]));
            drawRoute(ManhattanArray, shopMap, colorLine, colorTarget);
        }
        else {
            colorLine = '#d53f4e';
            colorTarget = '#eced4d';
            ManhattanArray = calculateManhattanArray(10, shopMap, finditScreenX, finditScreenY, Math.floor((parseInt(cookieArray2[1]) + parseInt(cookieArray2[2])) / 2), parseInt(cookieArray2[3]));
            drawRoute(ManhattanArray, shopMap, colorLine, colorTarget);
            colorLine = '#83ffc8';
            colorTarget = '#ff83dc';
            ManhattanArray = calculateManhattanArray(10, shopMap, ManhattanArray[ManhattanArray.length - 1][0], ManhattanArray[ManhattanArray.length - 1][1], Math.floor((parseInt(cookieArray1[1]) + parseInt(cookieArray1[2])) / 2), parseInt(cookieArray1[3]));
            drawRoute(ManhattanArray, shopMap, colorLine, colorTarget);
        }
    }
    else if ($.cookie("mapRouteCoordinates1") != null) {

        cookie1 = cookie1.split('&');

        for (i = 0; i < cookie1.length; i++) {
            cookies1 = cookie1[i].split('=');
            cookieArray1[i] = cookies1[1];
        }
        colorLine = '#d53f4e';
        colorTarget = '#eced4d';
        var ManhattanArray = calculateManhattanArray(10, shopMap, finditScreenX, finditScreenY, Math.floor((parseInt(cookieArray1[1]) + parseInt(cookieArray1[2])) / 2), parseInt(cookieArray1[3]));
        drawRoute(ManhattanArray, shopMap, colorLine, colorTarget);
    }
}

function getMap() {
    var txtButton = $('#addRoute').val();


    var cookie3, cookieArray3 = [], cookies3 = [];
    var cookie2, cookieArray2 = [], cookies2 = [];
    var cookie1, cookieArray1 = [], cookies1 = [];

    cookie3 = $.cookie("mapRouteCoordinates3");
    cookie2 = $.cookie("mapRouteCoordinates2");
    cookie1 = $.cookie("mapRouteCoordinates1");

    var id = parseInt(location.pathname.substring(location.pathname.lastIndexOf("/") + 1));
    $.ajax({
        type: 'POST',
        url: '/ProductDetails/CreateRoute',
        data: JSON.stringify({ id: id }),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {

            if (txtButton == 'Rotaya ekle') {
                if (cookie1 == null || cookie2 == null || cookie3 == null) {
                    $('#addRoute').removeClass('btn-success');
                    $('#addRoute').addClass('btn-danger');
                    $('#addRoute').val('Rotadan çıkart');
                    $('#inf').html('');
                    $('#inf').html('Rotaya ürün eklendi');
                    $.post('../ShowProductShelfInformation',
                        {
                            productID: id
                        },
                        function (data) {
                            $('#modelView .modal-body').html(data);
                            $('#modelView').modal('show');
                        });
                    $.cookie.raw = true;
                    var obj = { ID: id, startXCoordinate: result.xStartCoordinate, endXCoordinate: result.xEndCoordinate, yCoordinate: result.yCoordinate };
                    if (cookie1 == null) {
                        $.cookie("mapRouteCoordinates1", $.param(obj), { expires: 10 });
                    }
                    else if (cookie2 == null) {
                        $.cookie("mapRouteCoordinates2", $.param(obj), { expires: 10 });
                    }
                    else if (cookie3 == null) {
                        $.cookie("mapRouteCoordinates3", $.param(obj), { expires: 10 });
                    }

                    drawRouteWithCookies();
                }
                else {
                    alert('Daha fazla ürün ekleyemezsiniz.');
                }
            }
            else {
                $('#addRoute').removeClass('btn-danger');
                $('#addRoute').addClass('btn-success');
                $('#addRoute').val('Rotaya ekle');
                $('#inf').html('');
                $('#inf').html('Rotadan ürün çıkartıldı.');

                //cookieler doldurulduğu için tekrar değerleri çekildi.
                cookie3 = $.cookie("mapRouteCoordinates3");
                cookie2 = $.cookie("mapRouteCoordinates2");
                cookie1 = $.cookie("mapRouteCoordinates1");
                var changerCookie;
                $.cookie.raw = true;
                if (cookie3 != null) {
                    cookie3 = cookie3.split('&');
                    cookie2 = cookie2.split('&');
                    cookie1 = cookie1.split('&');

                    for (i = 0; i < cookie3.length; i++) {
                        cookies3 = cookie3[i].split('=');
                        cookieArray3[i] = cookies3[1];

                        cookies2 = cookie2[i].split('=');
                        cookieArray2[i] = cookies2[1];

                        cookies1 = cookie1[i].split('=');
                        cookieArray1[i] = cookies1[1];
                    }

                    if (cookieArray3[0] == id) {
                        $.cookie("mapRouteCoordinates3", null, { expires: -1 });
                    }
                    else if (cookieArray2[0] == id) {
                        changerCookie = $.cookie("mapRouteCoordinates3");
                        $.cookie("mapRouteCoordinates2", changerCookie, { expires: 10 });
                        $.cookie("mapRouteCoordinates3", null, { expires: -1 });
                    }
                    else if (cookieArray1[0] == id) {
                        changerCookie = $.cookie("mapRouteCoordinates2");
                        $.cookie("mapRouteCoordinates1", changerCookie, { expires: 10 });
                        changerCookie = $.cookie("mapRouteCoordinates3");
                        $.cookie("mapRouteCoordinates2", changerCookie, { expires: 10 });
                        $.cookie("mapRouteCoordinates3", null, { expires: -1 });
                    }
                    drawRouteWithCookies();
                }
                else if (cookie2 != null) {
                    cookie2 = cookie2.split('&');
                    cookie1 = cookie1.split('&');

                    for (i = 0; i < cookie2.length; i++) {
                        cookies2 = cookie2[i].split('=');
                        cookieArray2[i] = cookies2[1];

                        cookies1 = cookie1[i].split('=');
                        cookieArray1[i] = cookies1[1];
                    }

                    if (cookieArray2[0] == id) {
                        $.cookie("mapRouteCoordinates2", null, { expires: -1 });
                    }
                    else if (cookieArray1[0] == id) {
                        changerCookie = $.cookie("mapRouteCoordinates2");
                        $.cookie("mapRouteCoordinates1", changerCookie, { expires: 10 });
                        $.cookie("mapRouteCoordinates2", null, { expires: -1 });
                    }
                    drawRouteWithCookies();
                }
                else if (cookie1 != null) {
                    $.cookie("mapRouteCoordinates1", null, { expires: -1 });
                    drawRouteWithCookies();
                }
            }
        }
    });
}

function drawMap(finditScreenX, finditScreenY) {
    var id = parseInt(location.pathname.substring(location.pathname.lastIndexOf("/") + 1));
    var shopMap;
    //bu urlimizi post et
    $.ajax({
        type: 'POST',
        async: false,
        url: '/ProductDetails/MapShown',
        data: JSON.stringify({ id: id }),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            //Mağaza haritası bilgileri
            var shelfL = result[0].shelfLength;
            var shopW = result[0].mapWidth;
            var shopH = result[0].mapHeight;

            var canvasW = $('#game').width();
            var canvasH = $('#game').height();

            var shelfs = new Array();
            var shelfTotalCount;
            if (shelfL >= 100) {
                shelfL = shelfL / 100;
            }
            else {
                shelfL = 1;
            }

            var tileW = canvasW / shopW, tileH = canvasH / shopH;
            shopMap = new Array(shopH);

            //boş harita dizisinin oluşturulması
            for (var i = 0; i < shopH; i++) {
                shopMap[i] = new Array(shopW)
                for (var j = 0; j < shopW; j++) {
                    shopMap[i][j] = 0
                }
            }
            //tepe rafların oluşturulması
            for (var i = 0; i < shopW; i++) {
                shopMap[0][i] = 1
            }

            // köşe rafların oluşturulması
            for (var i = 0; i < shopH - 4; i++) {
                shopMap[i][0] = 1
                shopMap[i][shopW - 1] = 1
            }

            //haritada sağ tarafta tekli raf olacak mı olmayacak mı bilgisi
            var flag = false
            var shelfCountinOneRow = Math.ceil((shopW - 5) / 3)
            if (Math.round((shopW - 5) / 3) != (shopW - 5) / 3) {
                flag = true
            }

            //raf sayısı
            var shelfCount = 0;
            var counter = 0;
            var rowShelfCounter = 0;

            var counterCorridor = 0
            var corridor = 0

            // iç rafların oluşturulması
            for (var i = 3; i < shopH - 4; i++) {
                for (var j = 3; j < shopW - 3; j++) {
                    if (flag == true) {
                        if (j == shopW - 4) {
                            if (shopMap[i][j - 1] == 0 && shopMap[i][j + 1] == 0) {
                                shopMap[i][j] = 1;
                            }
                            if (shopMap[i][j - 1] == 1 && shopMap[i][j + 1] == 0) {
                                shopMap[i][j] = 0;
                                shopMap[i][j + 1] = 1;
                            }
                            counter++;
                        }
                        else {
                            if (counterCorridor != 2) {
                                shopMap[i][j] = 1
                                counterCorridor++
                                counter++;
                            } else {
                                counterCorridor = 0
                            }
                        }
                    }
                    else {
                        if (counterCorridor != 2) {
                            shopMap[i][j] = 1
                            counter++;
                            counterCorridor++
                        } else {
                            counterCorridor = 0
                        }
                    }
                }
                corridor++
                if (corridor % shelfL == 0) {
                    i = i + 3
                    shelfCount += counter;
                    corridor = 0
                    rowShelfCounter++;
                }
                counter = 0;
                counterCorridor = 0
            }

            shelfs.length = shelfCount;
            var startX = 4, endX = 0, startY = 4, endY = 4;
            var shelfIndex = 0;
            //raf koordinat tespiti
            for (var i = 0; i < rowShelfCounter; i++) {
                endX = startX + shelfL - 1;
                for (var j = 0; j < shelfCount / rowShelfCounter; j++) {
                    shelfs[shelfIndex] = new Array(shelfIndex + 1, startX, endX, startY, endY);
                    if (flag == true) {
                        if (j == (shelfCount / rowShelfCounter) - 1) {
                            startY += 2;
                            endY = startY;
                        }
                        else {
                            if ((j + 1) % 2 == 0) {
                                startY += 2;
                                endY = startY;
                            }
                            else {
                                startY += 1;
                                endY = startY;
                            }
                        }
                    }
                    else {
                        if ((j + 1) % 2 == 0) {
                            startY += 2;
                            endY = startY;
                        }
                        else {
                            startY += 1;
                            endY = startY;
                        }
                    }
                    shelfIndex++;
                }
                startY = endY = 4;
                startX = endX + 4;
            }
            //findit ekranı
            shopMap[finditScreenX][finditScreenY] = 2;
            ctx = document.getElementById('game').getContext('2d');
            ctx.font = 'bold 16pt sans-serif';

            if (ctx == null) {
                return
            }
            //rafları çizme
            for (var x = 0; x < shopH; ++x) {
                for (var y = 0; y < shopW; ++y) {
                    switch (shopMap[x][y]) {
                        case 0:
                            ctx.fillStyle = '#fff'
                            break
                        case 2:
                            ctx.fillStyle = '#3dd1ff';
                            break
                        default:
                            ctx.fillStyle = '#5aa457'
                    }
                    ctx.fillRect(y * tileW, x * tileH, tileW, tileH)
                }
            }
            //raf numaralarını yazdırma
            var rowCount = 0;
            for (var i = 0; i < shelfs.length; i++) {
                if (i < shelfCount / rowShelfCounter) {
                    ctx.fillText(shelfs[i][0], (shelfs[i][3] * tileW) - 22, (shelfL - 1) * tileH);
                }
                if (i >= shelfCount / rowShelfCounter) {
                    ctx.fillText(shelfs[i][0], (shelfs[i][3] * tileW) - 26, (shelfL - 1 + rowCount) * tileH);
                }
                if ((i + 1) % (shelfCount / rowShelfCounter) == 0) {
                    rowCount += (shelfL + 3);
                }
            }
        },
        error: function (hata, ajaxOptions, thrownError) {
            alert(hata.status);
            alert(thrownError);
            alert(hata.responseText);
        }
    });
    return shopMap;
}

function calculateManhattanArray(shelfL, shopMap, finditScreenX, finditScreenY, productXCoordinate, productYCoordinate) {
    var ManhattanArray = new Array();
    ManhattanArray[0] = new Array(2);
    ManhattanArray[0][0] = finditScreenX; // StartX Noktası
    ManhattanArray[0][1] = finditScreenY; // StartY Noktası

    var lineEnd = 0;

    //Findit başlangıç koordinatı (başlangıç noktası)
    var xStart = finditScreenX;
    var yStart = finditScreenY;

    //Ürün rafı koordinatı (varış noktası)
    var xEnd = productXCoordinate;
    var yEnd = productYCoordinate - 1;

    if (shopMap[xEnd][yEnd - 1] == 0) {
        yEnd = yEnd - 1;
        lineEnd = 30;
    }
    else {
        yEnd += 1;
    }
    var xStart1 = 0, xStart2 = 0;
    var yStart1 = 0, yStart2 = 0;

    var ManhattanDist1;
    var ManhattanDist2;
    var index = 1;

    if (xStart == 2 && yStart == 14) {
        while (1) {
            if (yStart >= yEnd) {
                if (xStart <= xEnd) {
                    //Birinci köşe noktası
                    yStart1 = yStart - 3;
                    xStart1 = xStart;

                    //İkinci köşe noktası
                    yStart2 = yStart;
                    xStart2 = xStart + 13;
                }
                else {
                    //Birinci köşe noktası
                    yStart1 = yStart - 3;
                    xStart1 = xStart;

                    //İkinci köşe noktası
                    yStart2 = yStart;
                    xStart2 = xStart - 13;
                }
            }
            else {
                if (xStart >= xEnd) {
                    //Birinci köşe noktası
                    yStart1 = yStart + 3;
                    xStart1 = xStart;

                    //İkinci köşe noktası
                    yStart2 = yStart;
                    xStart2 = xStart - 13;
                }
                else {
                    //Birinci köşe noktası
                    yStart1 = yStart + 3;
                    xStart1 = xStart;

                    //İkinci köşe noktası
                    yStart2 = yStart;
                    xStart2 = xStart + 13;
                }
            }
            if ((Math.abs(xStart - xEnd) + Math.abs(yStart - yEnd)) <= (shelfL - 3)) {
                break;
            }
            else {
                //İki köşe noktası için manhattan distance hesapla ve küçük olanı yeni iterasyonum olarak belirle
                ManhattanDist1 = Math.abs(xStart1 - xEnd) + Math.abs(yStart1 - yEnd);
                ManhattanDist2 = Math.abs(xStart2 - xEnd) + Math.abs(yStart2 - yEnd);
                if (ManhattanDist1 > ManhattanDist2) {
                    xStart = xStart2;
                    yStart = yStart2;
                }
                else {
                    xStart = xStart1;
                    yStart = yStart1;
                }
                ManhattanArray[index] = new Array(2)
                //Manhattan Noktasının X koordinatı
                ManhattanArray[index][0] = xStart;
                //Manhattan Noktasının Y koordinatı
                ManhattanArray[index][1] = yStart;
            }
            index++;
        }
    }
    else {
        var counter = 0;
        while (1) {
            if (counter == 0) {
                yStart1 = yStart;
                yStart2 = yStart;

                xStart1 = xStart + 6;
                xStart2 = xStart + 6;
                counter++;
            }
            else {
                if (yStart >= yEnd) {
                    if (xStart <= xEnd) {
                        //Birinci köşe noktası
                        yStart1 = yStart - 3;
                        xStart1 = xStart;

                        //İkinci köşe noktası
                        yStart2 = yStart;
                        xStart2 = xStart + 13;
                    }
                    else {
                        //Birinci köşe noktası
                        yStart1 = yStart - 3;
                        xStart1 = xStart;

                        //İkinci köşe noktası
                        yStart2 = yStart;
                        xStart2 = xStart - 13;
                    }
                }
                else {
                    if (xStart >= xEnd) {
                        //Birinci köşe noktası
                        yStart1 = yStart + 3;
                        xStart1 = xStart;

                        //İkinci köşe noktası
                        yStart2 = yStart;
                        xStart2 = xStart - 13;
                    }
                    else {
                        //Birinci köşe noktası
                        yStart1 = yStart + 3;
                        xStart1 = xStart;

                        //İkinci köşe noktası
                        yStart2 = yStart;
                        xStart2 = xStart + 13;
                    }
                }

            }
            if ((Math.abs(xStart - xEnd) + Math.abs(yStart - yEnd)) <= (shelfL - 3)) {
                break;
            }
            else {
                //İki köşe noktası için manhattan distance hesapla ve küçük olanı yeni iterasyonum olarak belirle
                ManhattanDist1 = Math.abs(xStart1 - xEnd) + Math.abs(yStart1 - yEnd);
                ManhattanDist2 = Math.abs(xStart2 - xEnd) + Math.abs(yStart2 - yEnd);
                if (ManhattanDist1 > ManhattanDist2) {
                    xStart = xStart2;
                    yStart = yStart2;
                }
                else {
                    xStart = xStart1;
                    yStart = yStart1;
                }
                ManhattanArray[index] = new Array(2)
                //Manhattan Noktasının X koordinatı
                ManhattanArray[index][0] = xStart;
                //Manhattan Noktasının Y koordinatı
                ManhattanArray[index][1] = yStart;
            }
            index++;
        }
    }

    ManhattanArray[index] = new Array(3)
    ManhattanArray[index][0] = xEnd;
    ManhattanArray[index][1] = yEnd;
    ManhattanArray[index][2] = lineEnd;
    return ManhattanArray;
}

function drawRoute(ManhattanArray, shopMap, colorLine, colorTarget) {
    //rotayı çizme
    var tileW = $('#game').width() / (shopMap[0].length);
    var tileH = $('#game').height() / (shopMap.length);
    ctx = document.getElementById('game').getContext('2d');

    //hedef ürün konumu işaretleme
    if (ManhattanArray[ManhattanArray.length - 1][2] == 30) {
        ctx.fillStyle = colorTarget;
        ctx.fillRect((ManhattanArray[ManhattanArray.length - 1][1] + 1) * tileW, ManhattanArray[ManhattanArray.length - 1][0] * tileH, tileW, tileH)
    }
    else {
        ctx.fillStyle = colorTarget;
        ctx.fillRect((ManhattanArray[ManhattanArray.length - 1][1] - 1) * tileW, ManhattanArray[ManhattanArray.length - 1][0] * tileH, tileW, tileH)
    }


    var sX = ManhattanArray[0][0] * tileH;
    var sY = ManhattanArray[0][1] * tileW;

    ctx.beginPath();
    ctx.moveTo(sY + 15, sX);

    for (var i = 1; i < ManhattanArray.length - 1; i++) {
        ctx.lineTo(ManhattanArray[i][1] * tileW + 15, ManhattanArray[i][0] * tileH);
    }
    var xEnd = ManhattanArray[ManhattanArray.length - 1][0];
    var yEnd = ManhattanArray[ManhattanArray.length - 1][1];
    ctx.lineTo(yEnd * tileW + 15, xEnd * tileH);
    ctx.lineTo(yEnd * tileW + ManhattanArray[ManhattanArray.length - 1][2], xEnd * tileH);
    ctx.lineWidth = 6;
    ctx.strokeStyle = colorLine;
    ctx.stroke();

}
