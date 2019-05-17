var shelfs = new Array();

function drawShopMap() {
    var ctx = null;
    var shelfL = document.getElementById('shelfLength').value;
    var shopW = document.getElementById('shopWidth').value;
    var shopH = document.getElementById('shopHeight').value;
    var canvasW =$('#game').width();
    var canvasH = $('#game').height();

    var shelfTotalCount;
    if (shelfL >= 100) {
        shelfL = shelfL / 100;
    }
    else {
        shelfL = 1;
    }

    var tileW = canvasW / shopW, tileH = canvasH / shopH;
    var shopMap = new Array(shopH)

    for (var i = 0; i < shopH; i++) {
        shopMap[i] = new Array(shopW)
        for (var j = 0; j < shopW; j++) {
            shopMap[i][j] = 0
        }
    }

    for (var i = 0; i < shopW; i++) {
        shopMap[0][i] = 1
    }

    // köşe rafların oluşturulması
    for (var i = 0; i < shopH - 4; i++) {
        shopMap[i][0] = 1
        shopMap[i][shopW - 1] = 1
    }

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
    var startX = 4, endX = 0, startY = 4;
    var shelfIndex = 0;

    //raf koordinat tespiti
    for (var i = 0; i < rowShelfCounter; i++) {
        endX = startX + shelfL - 1;
        for (var j = 0; j < shelfCount / rowShelfCounter; j++) {
            shelfs[shelfIndex] = new Array(shelfIndex + 1, startX, endX, startY);
            if (flag == true) {
                if (j == (shelfCount / rowShelfCounter) - 1) {
                    startY += 2;
                }
                else {
                    if ((j + 1) % 2 == 0) {
                        startY += 2;
                    }
                    else {
                        startY += 1;
                    }
                }
            }
            else {
                if ((j + 1) % 2 == 0) {
                    startY += 2;
                }
                else {
                    startY += 1;
                }
            }
            shelfIndex++;
        }
        startY = 4;
        startX = endX + 4;
    }

    ctx = document.getElementById('game').getContext('2d');
    requestAnimationFrame(drawGame);
    ctx.font = 'bold 20pt sans-serif';

    function drawGame() {
        if (ctx == null) {
            return
        }

        for (var x = 0; x < shopH; ++x) {
            for (var y = 0; y < shopW; ++y) {
                switch (shopMap[x][y]) {
                    case 0:
                        ctx.fillStyle = '#fff'
                        break
                    default:
                        ctx.fillStyle = '#1f497d'
                }
                ctx.fillRect(y * tileW, x * tileH, tileW, tileH)
            }
        }

        var rowCount = 0;
        for (var i = 0; i < shelfs.length; i++) {
            if (i < shelfCount / rowShelfCounter) {
                ctx.fillText(shelfs[i][0], (shelfs[i][3] * tileW) - 31, (shelfL - 1) * tileH);
            }
            if (i >= shelfCount / rowShelfCounter) {
                ctx.fillText(shelfs[i][0], (shelfs[i][3] * tileW) - 32, (shelfL - 1 + rowCount) * tileH);
            }
            if ((i + 1) % (shelfCount / rowShelfCounter) == 0) {
                rowCount += (shelfL + 3);
            }
        }
        requestAnimationFrame(drawGame)
    }

    $("#addMapButton").click(function () {
       
        $.ajax({
            url: '/Map/MapCreate',
            data: { shelfs },
            dataType: "JSON",
            traditional : true,
            success: function (message) {
                if (message == "OK") {
                    alert("success");
                }
                else {
                    alert("fail");
                }
            }
        });
    });
}

function addShelf() {
    var categoryID = $("#categoryID").val();
    var shelfID = $("#shelfID").val();
    $.ajax({
        type: 'POST',
        url: '/Map/EditingShelfs',
        data: JSON.stringify({
            shelfID: shelfID,
            categoryID: categoryID
        }),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            if (result == "OK") {
                alert("Raf düzenlendi.");
                location.reload();
            }
            else {
                alert("Raf düzenlenemedi.");
            }
        }
    });
}