const uri = "./api/guest";
let guest = null;
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

$(document).ready(function () {
    getData();
});

function getData() {
    $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        success: function (data) {
            const tBody = $("#guests");
            $(tBody).empty();

            getCount(data.length);

            $.each(data, function (key, item) {
                const tr = $("<tr></tr>")
                    .append($("<td></td>").text(item.firstName))
                    .append($("<td></td>").text(item.lastName))                    
                    .append($("<td></td>").text(item.middleName))
                    .append($("<td></td>").text(item.dateIn))
                    .append($("<td></td>").text(item.dateOut))
                  
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
                            $("<button>Редактировать</button>").on("click", function () {
                                editItem(item.id);
                            })
                        )
                    )
                    .append(
                        $("<td></td>").append(
                            $("<button>Удалить</button>").on("click", function () {
                                deleteItem(item.id);
                            })
                        )
                    );

                tr.appendTo(tBody);
            });

            guest = data;
        }
    });
}

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
            $("#add-name").val("");
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
    $.each(guest, function (key, item) {
        if (item.id === id) {
            $("#edit-name").val(item.lastName);
            $("#edit-id").val(item.id);
          
        }
    });
    $("#spoiler").css({ display: "block" });
}

$(".my-form").on("submit", function () {
    const item = {
        name: $("#edit-name").val(),
        isComplete: $("#edit-isComplete").is(":checked"),
        id: $("#edit-id").val()
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

$(".my-formа").on("submit", function () {
    const item = {
        name: $("#edit-name").val(),
        isComplete: $("#edit-isComplete").is(":checked"),
        id: $("#edit-id").val()
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

function closeInput() {
    $("#spoiler").css({ display: "none" });
}