function AjaxCallHelper(form, callback, completeCallback) {
    $.ajax({
        url: form.action,
        type: form.method,
        dataType: "html",
        data: $(form).serialize(),
        success: callback
    }).always(completeCallback);
}

var HtmlConstructor = HtmlConstructor || {};

HtmlConstructor.ConstructDockEntryWebResult = function(res) {
    if (res == null || res == "") {
        return "";
    }
    var table = "<table cellpadding=\"3\" cellspacing=\"0\"> <tr>"
                    + "<th class=\"first\">箱号</th>"
                    + "<th>箱经营人名称</th>"
                    + "<th>箱经营人代码</th>"
                    + "<th>船名</th>"
                    + "<th>航次</th>"
                    + "<th>进/出口</th>"
                    + "<th>进出门场站</th>"
                    + "<th>进出门目的</th>"
                    + "<th>进出门时间</th>"
                + "</tr>";
    var currentBgColor = ""
    for (var i = 0; i < res.length ; i++) {
        if (i > 0 && res[i].ContainerNumber != res[i - 1].ContainerNumber) {
            if (currentBgColor == "")
                currentBgColor = "white";
            else
                currentBgColor = "";
        }

        var htmlRow = "<tr " + "class=\"" + currentBgColor +"\">"
                    + "<td class=\"first\">" + res[i].ContainerNumber + "</td>"
                    + "<td>" + res[i].OperationName + "</td>"
                    + "<td>" + res[i].OperationCode + "</td>"
                    + "<td>" + res[i].Conveyance + "</td>"
                    + "<td>" + res[i].VoyageNumber + "</td>"
                    + "<td>" + res[i].Type + "</td>"
                    + "<td>" + res[i].Dock + "</td>"
                    + "<td>" + res[i].Target + "</td>"
                    + "<td>" + renderTime(res[i].Time) + "</td>"
                    + "</tr>";
        table += htmlRow;
    }

    table += "</table>"
    return table;
}

HtmlConstructor.ConstructPrerecordWarrantWebResult = function (res) {
    if (res == null || res == "") {
        return "";
    }
    var table = "<table cellpadding=\"3\" cellspacing=\"0\"> <tr>"
                    + "<th class=\"first\">提单号</th>"
                    + "<th>船名</th>"
                    + "<th>航次</th>"
                    + "<th>预配舱单状态</th>"
                + "</tr>";
    var currentBgColor = ""
    for (var i = 0; i < res.length ; i++) {
        if (i > 0 && res[i].BillNumber != res[i - 1].BillNumber) {
            if (currentBgColor == "")
                currentBgColor = "white";
            else
                currentBgColor = "";
        }

        var htmlRow = "<tr " + "class=\"" + currentBgColor + "\">"
                    + "<td class=\"first\">" + res[i].BillNumber + "</td>"
                    + "<td>" + res[i].Conveyance + "</td>"
                    + "<td>" + res[i].VoyageNumber + "</td>"
                    + "<td>" + res[i].PrerecordWarrantStatus + "</td>"
                    + "</tr>";
        table += htmlRow;
    }
    table += "</table>";
    return table;
}

HtmlConstructor.ConstructLeaveDateWebResult = function (res) {
    if (res == null || res == "") {
        return "";
    }
    var table = "<table cellpadding=\"3\" cellspacing=\"0\"> <tr>"
                    + "<th class=\"first\">港区</th>"
                    + "<th>船名</th>"
                    + "<th>航次</th>"
                    + "<th>计划离港时间</th>"
                    + "<th>实际离港时间</th>"
                    + "<th>计划到达时间</th>"
                    + "<th>实际到达时间</th>"
                    + "<th>已关闭</th>"
                + "</tr>";
    var currentBgColor = ""
    for (var i = 0; i < res.length ; i++) {
        var htmlRow = "<tr " + "class=\"" + currentBgColor + "\">"
                    + "<td class=\"first\">" + res[i].Dock + "</td>"
                    + "<td>" + res[i].Conveyance + "</td>"
                    + "<td>" + res[i].VoyageNumber + "</td>"
                    + "<td>" + res[i].PlanLeavingDate + "</td>"
                    + "<td>" + res[i].ActualLeavingDate + "</td>"
                    + "<td>" + res[i].PlanArrivalDate + "</td>"
                    + "<td>" + res[i].ActualArrivalDate + "</td>"
                    + "<td>" + res[i].IsClosed + "</td>"
                    + "</tr>";
        table += htmlRow;
    }
    table += "</table>";
    return table;
}

HtmlConstructor.ConstructEmptyResult = function () {
    return "<div class=\"SearchEmptyResult\">没有搜索结果，请稍候重试！</div>"
}

HtmlConstructor.ConstructAdmissionWebResult = function (res) {
    if (res == null || res == "") {
        return "";
    }
    var table = "<table cellpadding=\"3\" cellspacing=\"0\"> <tr>"
                    + "<th class=\"first\">船名</th>"
                    + "<th>航次</th>"
                    + "<th>提单号</th>"
                    + "<th>箱号</th>"
                    + "<th>报关单号</th>"
                    + "<th>作业码头</th>"
                    + "<th>海关放行状态</th>"
                    + "<th>EDI中心接收时间</th>"
                    + "<th>码头回执状态</th>"
                    + "<th>序列号</th>"
                + "</tr>";
    var currentBgColor = ""
    for (var i = 0; i < res.length ; i++) {
        if (i > 0 && res[i].ContainerNumber != res[i - 1].ContainerNumber) {
            if (currentBgColor == "")
                currentBgColor = "white";
            else
                currentBgColor = "";
        }

        var htmlRow = "<tr " + "class=\"" + currentBgColor + "\">"
                    + "<td class=\"first\">" + res[i].Conveyance + "</td>"
                    + "<td>" + res[i].VoyageNumber + "</td>"
                    + "<td>" + res[i].BillNumber + "</td>"
                    + "<td>" + res[i].ContainerNumber + "</td>"
                    + "<td>" + res[i].DeclarationNumber + "</td>"
                    + "<td>" + res[i].Dock + "</td>"
                    + "<td>" + res[i].AdmissionStatus + "</td>"
                    + "<td>" + renderTime(res[i].EDITime) + "</td>"
                    + "<td>" + res[i].ReceivedStatus + "</td>"
                    + "<td>" + res[i].SerialNumber + "</td>"
                    + "</tr>";
        table += htmlRow;
    }

    table += "</table>";
    return table;
}

HtmlConstructor.ConstructLandingWebResult = function (res) {
    if (res == null || res == "") {
        return "";
    }
    var table = "";
    //LadingBillNumberWebEntity
    if (res.LadingBillNumberWebEntity != null) {
        table += "<ul>"
                   + "<li>" + "船名: " + res.LadingBillNumberWebEntity.Conveyance + "</li>"
                   + "<li>" + "航次: " + res.LadingBillNumberWebEntity.VoyageNumber + "</li>"
                   + "<li>" + "提单号: " + res.LadingBillNumberWebEntity.BillNumber + "</li>"
                   + "<li>" + "重量: " + res.LadingBillNumberWebEntity.TotalWeight + "</li>"
                   + "<li>" + "件数: " + res.LadingBillNumberWebEntity.TotalAmount + "</li>"
               + "</ul>";

        table += "<table cellpadding=\"3\" cellspacing=\"0\"> <tr>"
                    + "<th class=\"first\">箱号</th>"
                    + "<th>重量</th>"
                    + "<th>件数</th>"
                    + "<th>海关回执</th>"
                    + "<th>海关接收回执时间</th>"
                    + "<th>箱经营人</th>"
                    + "<th>卸港代码</th>"
                    + "<th>EDICOSTRP号</th>"
                + "</tr>";

        var currentBgColor = "";
        for (var i = 0; i < res.LadingBillNumberWebEntity.LadingDetail.length ; i++) {
            var htmlRow = "<tr " + "class=\"" + currentBgColor + "\">"
                    + "<td class=\"first\">" + res.LadingBillNumberWebEntity.LadingDetail[i].ContainerNumber + "</td>"
                    + "<td>" + res.LadingBillNumberWebEntity.LadingDetail[i].Weight + "</td>"
                    + "<td>" + res.LadingBillNumberWebEntity.LadingDetail[i].Amount + "</td>"
                    + "<td>" + res.LadingBillNumberWebEntity.LadingDetail[i].CustomsInfo + "</td>"
                    + "<td>" + renderTime(res.LadingBillNumberWebEntity.LadingDetail[i].ReveivedDate) + "</td>"
                    + "<td>" + res.LadingBillNumberWebEntity.LadingDetail[i].Owner + "</td>"
                    + "<td>" + res.LadingBillNumberWebEntity.LadingDetail[i].LadingCode + "</td>"
                    + "<td>" + res.LadingBillNumberWebEntity.LadingDetail[i].COSTRPNumber + "</td>"
                    + "</tr>";
            table += htmlRow;
        }
        table += "</table>";
    }
    // LadingContainerNumberWebEntity
    if (res.LadingContainerNumberWebEntity != null) {
        table += "<ul>"
                   + "<li>" + "船名: " + res.LadingContainerNumberWebEntity.Conveyance + "</li>"
                   + "<li>" + "航次: " + res.LadingContainerNumberWebEntity.VoyageNumber + "</li>"
                   + "<li>" + "箱号: " + res.LadingContainerNumberWebEntity.ContainerNumber + "</li>"
                   + "<li>" + "进港时间: " + res.LadingContainerNumberWebEntity.LadingDate + "</li>"
                   + "<li>" + "进港地点: " + res.LadingContainerNumberWebEntity.Dock + "</li>"
                   + "<li>" + "箱经营人代码: " + res.LadingContainerNumberWebEntity.Owner + "</li>"
                   + "<li>" + "COSTCO报文号: " + res.LadingContainerNumberWebEntity.COSTRPSentNumber + "</li>"
                   + "<li>" + "COSTCO报文发送时间: " + res.LadingContainerNumberWebEntity.COSTRPSentDate + "</li>"
               + "</ul>";
        table += "<table cellpadding=\"3\" cellspacing=\"0\"> <tr>"
                    + "<th class=\"first\">提单号</th>"
                    + "<th>重量</th>"
                    + "<th>件数</th>"
                    + "<th>海关回执</th>"
                    + "<th>海关接收回执时间</th>"
                    + "<th>卸港代码</th>"
                    + "<th>COSTRP号</th>"
                + "</tr>";

        var currentBgColor = "";
        for (var i = 0; i < res.LadingContainerNumberWebEntity.LadingDetail.length ; i++) {
            var htmlRow = "<tr " + "class=\"" + currentBgColor + "\">"
                    + "<td class=\"first\">" + res.LadingContainerNumberWebEntity.LadingDetail[i].BillNumber + "</td>"
                    + "<td>" + res.LadingContainerNumberWebEntity.LadingDetail[i].Weight + "</td>"
                    + "<td>" + res.LadingContainerNumberWebEntity.LadingDetail[i].Amount + "</td>"
                    + "<td>" + res.LadingContainerNumberWebEntity.LadingDetail[i].CustomsInfo + "</td>"
                    + "<td>" + res.LadingContainerNumberWebEntity.LadingDetail[i].ReveivedDate + "</td>"
                    + "<td>" + res.LadingContainerNumberWebEntity.LadingDetail[i].LadingCode + "</td>"
                    + "<td>" + res.LadingContainerNumberWebEntity.LadingDetail[i].COSTRPNumber + "</td>"
                    + "</tr>";
            table += htmlRow;
        }
        table += "</table>";
    }
    return table;
}

function renderTime(date) {
    var da = new Date(parseInt(date.replace("/Date(", "").replace(")/", "").split("+")[0]));
    return da.getFullYear() + "-" + (da.getMonth() + 1) + "-" + da.getDate() + " " + da.getHours() + ":" + da.getSeconds() + ":" + da.getMinutes();
}