let uri = "../api/room";
var sortorder = "";
var searchString = "";
let room = null;
let btnRoomId = null;

function getCount(data) {
    const el = $("#counter");
    let id = "hotelId";
    let hotelName = "hotelName";
    let rooms = "rooms";
    if (data) {
        if (data > 1) {
            hotelName = "hotelName";
            rooms = "rooms";

        }
        el.text(data + " " + hotelName + " " + rooms);
    } else {
        el.text("No " + hotelName + rooms);
    }
}


   
function getSortorder() {    
    var url_string = window.location.href;   
    var url = new URL(url_string);
    sortorder = url.searchParams.get("sortorder");
    if ((sortorder != "" || sortorder != null))
    {
        uri = "" + uri + "/?sortorder=" + sortorder;
    }
      
    return sortorder;
}


$(document).ready(function () {

    getData();
   
});

var status = true;
function roomStatus(status)
{
    if (status == true) {
        return "Свободен";
    }
    else
    {
        return "Занят";
    }
}

function getData() {  

    sortorder = getSortorder();

    
     $.ajax({
        type: "GET",
        url: uri ,      
        cache: false,
        success: function (data) {        
            const tBody = $("#rooms");
            $(tBody).empty();

            getCount(data.length);
           
            $.each(data, function (key, item) {
                const tr = $("<tr></tr>")                    
                    .append($("<td></td>").text(item.hotel.hotelName))
                    .append($("<td></td>").text(item.capacity))
                    .append($("<td></td>").text(item.typeNavigation.typeName))
                    .append($("<td></td>").text(roomStatus(item.status)))

                    .append($("<td></td>").text(item.hotelName))

                    .append(
                        $("<td></td>").append(
                        $("<button id="+item.roomId+">Регистрация гостя</button>").on("click", function () {
                            btnRoomId = this.id;
                            $("#spoiler").css({ display: "block" });
                            })
                        )
                    )

                    .append(
                        $("<td></td>").append(
                        $("<button id=" + item.roomId +">Освободить номер</button>").on("click", function () {
                            editItem(this.id);
                            })
                        )
                    );
                    

                tr.appendTo(tBody);
            });

            room = data;
        }
    });
}

$(document).ready(function () {
    $('#btnAdd').click(function () {

        addItem(btnRoomId);
    });
});

function addItem(id) {
   
    const item = {
        firstName: $("#first-name").val(),
        lastName: $("#last-name").val(),
        middleName: $("#middle-name").val(),
        dateIn: $("#date-in").val(),
        dateOut: $("#date-out").val(),
        roomId: id
    };

    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: uri,
        contentType: "application/json",
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Something went wrong!");
        },
        success: function (result) {
            getData();
           
        }
    });
    $("#spoiler").css({ display: "block" });
}

function deleteItem(id) {
    $.ajax({
        url: uri + "/" + id,
        type: "DELETE",
        success: function (result) {
            getData();
        }
    });
}

function editItem(id) {

    
    const item = {
                RoomId: id,
                Status: true
                  }
            
            $.ajax({
                url: "../api/room/" + id,
                type: "PUT",
                accepts: "application/json",
                contentType: "application/json",
                data: JSON.stringify(item),
                success: function (result) {
                    getData();
                }
            });

            closeInput();
       
   
   
}



$(".my-form").on("submit", function () {
    const item = {
        firstName: $("#first-name").val(),
        lastName: $("#last-name").val(),
        middleName: $("#middle-name").val(),
        dateIn: $("#date-in").val(),
        dateOut: $("#date-out").val(),
        roomId: $("#edit-id").val()
    };

    $.ajax({
        url: uri + "/" + $("#edit-id").val(),
        type: "PUT",
        accepts: "application/json",
        contentType: "application/json",
        data: JSON.stringify(item),
        success: function (result) {
            getData();
        }
    });

    closeInput();
    return false;
});

$(document).ready(function () {
    $('#btnSearch').click(function () {

        searchString = "?sortorder=&searchstring=" + $("#search-field").val();
        searchData(searchString);

    })
});

function searchData(searchString) {

  //  sortorder = getSortorder();


    $.ajax({
        type: "GET",
        url: uri + searchString,
        cache: false,
        success: function (data) {
            const tBody = $("#rooms");
            $(tBody).empty();

            getCount(data.length);

            $.each(data, function (key, item) {
                const tr = $("<tr></tr>")
                    .append($("<td></td>").text(item.hotel.hotelName))
                    .append($("<td></td>").text(item.capacity))
                    .append($("<td></td>").text(item.typeNavigation.typeName))
                    .append($("<td></td>").text(item.status))

                    .append($("<td></td>").text(item.hotelName))

                    .append(
                        $("<td></td>").append(
                            $("<button>Регистрация гостя</button>").on("click", function () {
                                addItem(item.id);
                            })
                        )
                    )

                    .append(
                        $("<td></td>").append(
                            $("<button>Освободить номер</button>").on("click", function () {
                                editItem(item.id);
                            })
                        )
                    );


                tr.appendTo(tBody);
            });

            room = data;
         //   $('#rooms').html(data);
        }
    });
}

function closeInput() {
    $("#spoiler").css({ display: "none" });
}

if ($("#hotel").attr("href") == "?sortorder=hotel_name_desc") {
    $("#hotel").attr("href", "?sortorder=hotel_name_asc");
}
else {
    $("#hotel").attr("href", "?sortorder=hotel_name_desc");
}