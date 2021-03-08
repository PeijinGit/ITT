```javascript
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title></title>
    <script src="https://unpkg.com/axios/dist/axios.min.js"></script>
    <style>
        #Modal {
            display: none;
            z-index: 999;
        }
        #updateModal {
            display: none;
            z-index: 999;
        }
    </style>
</head>
<body>
    <button onclick="openAdd()">ADD+</button>
    <table width="600" border="1" cellspacing="0">
        <thead>
            <tr>
                <th>Id</th>
                <th>EventName</th>
                <th>Creator</th>
                <th>Operate</th>
            </tr>
        </thead>
        <tbody id="tMain"></tbody>
    </table>
    <div id="Modal">
        EventName:<input id="eventNameInput" />
        CreatorId:<input id="creatorIdInput" />
        <button onclick="AddEvent()">Add</button>
    </div>
    <div id="updateModal">
        EventName:<input id="upeventNameInput" />
        CreatorId:<input id="upcreatorIdInput" />
        <button onclick="UpdateEvent()">Add</button>
    </div>
</body>
<script>
    window.onload = function (){
        initEvents()
    }

    function initEvents() {
        let tbody = document.getElementById("tMain")
        getAllEvents().then(function (result) {
            let joinE = [];
            for (var i = 0; i < result.length;i++) {
                joinE.push(createTable(result[i],i))
            }
            tbody.innerHTML = joinE.join("")
        })
            .catch(function (error) {
                console.log(error);
            });
    }

    async function getAllEvents() {
        let result;
        await axios.get('https://localhost:44398/Events/GetAllEvents')
            .then(function (response) {
                console.log(response);
                result = response.data
            })
            .catch(function (error) {
                console.log(error);
            });
        return result
    }

    function createTable(value, index) {
        //console.log("value",value)
        let rowElement = `<tr>
            <td>${index +1}</td>
            <td>${value.eventName}</td>
            <td>${value.userId}</td>
            <td>
                <button onclick="openUpdate('${value.eventName}','${ value.userId}')">Update</button>
                <button onclick="deleteEvent('${ value.id }')">Delete</button>
            </td>
        </tr >`
        return rowElement;
    }

    function deleteEvent(id) {
        if (confirm("Are u Sure?")) {
            axios.get('https://localhost:44398/Events/DeleteEvent?id=' + id)
                .then(function (response) {
                    if (response.data.status === 1) {
                        alert("SUCCESS")
                        location.reload()
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        }
    }

    function openAdd() {
        let modal = document.getElementById("Modal")
        modal.style.display = "block"
        console.log(modal)
    }

    function AddEvent() {
        let eventName = document.getElementById("eventNameInput").value
        let userId = document.getElementById("creatorIdInput").value
        
        if (eventName !== "" && userId !== "") {
            axios.post('https://localhost:44398/Events/AddEvent', { eventName: eventName, userId: parseInt(userId)  })
                .then(function (response) {
                    if (response.data.status === 1) {
                        alert("SUCCESS")
                        location.reload()
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        } else {
            alert("enter sth")
        }
    }

    function openUpdate(eventName,cretorId) {
        let updateModal = document.getElementById("updateModal")
        updateModal.style.display = "block"
        document.getElementById("upeventNameInput").value = eventName
        document.getElementById("upcreatorIdInput").value = cretorId
    }

    function updateEvent() {

    }

</script>
</html>
```

