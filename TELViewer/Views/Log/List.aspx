<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<TELViewer.Models.LogListPageModel>" %>
<%@ Import Namespace="TELViewer.Helpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>List</title>
    <%=Html.RegisterJs("~/Scripts/jquery-1.3.2.min.js")%>
    <%=Html.RegisterJs("~/Scripts/jquery-ui-1.7.2.custom.min.js")%>
    <%=Html.RegisterJs("~/Scripts/timepicker.js")%>
    
    <link rel="stylesheet" href="~/Content/jquery-ui-1.7.2.custom.css" type="text/css">
    <link rel="stylesheet" href="~/Content/site.css" type="text/css">
</head>
<body>
    <fieldset id="filters">
        <legend style="font-weight:bold; font-size:x-small; font-family:Arial; cursor:pointer;" id="filterPanelTitle">Filters </legend>
        <%using(Html.BeginForm("List", "Log", FormMethod.Get)) %>
        <table id="filterPanelTable" class="grid" summary="Query filters" style="width:auto;">
            <tbody>
                <tr style="height: 1px;">
                    <td colspan="4" />
                </tr>
            </tbody>
            <tbody>
                <tr class="logId">
                    <th scope="row">
                        <label>LogId</label>
                    </th>
                    <td class="mode">
                        equals
                    </td>
                    <td class="filter">
                       <%=Html.TextBox("Id", Model.Id, new { @class = "text", @size = "42" })%>
                    </td>
                </tr>
                <%--Logger--%>                 
                <tr class="Version">
                    <th scope="row">
                        <label>Logger</label>
                    </th>
                    <td class="mode">
                        is 
                    </td>                   
                    <td class="filter">
                        <%=Html.DropDownList("Logger", Model.GetLoggerList(Model.Logger), new { @class = "ddl" })%>                    
                    </td>
                </tr>                
                <%--Level--%>                 
                <tr class="Version">
                    <th scope="row">
                        <label>Level</label>
                    </th>
                    <td class="mode">
                        is
                    </td>                    
                    <td class="filter">
                        <%=Html.DropDownList("Level", Model.GetLevelList(Model.Level), new { @class = "ddl" })%>                    
                    </td>
                </tr>      
                <%--Message--%>                 
                <tr class="Message">
                    <th scope="row">
                        <label>Message</label>
                    </th>
                    <td class="mode">
                        like
                    </td>                    
                    <td class="filter">
                        <%=Html.TextBox("Message", Model.Message, new { @class = "text", @size = "42" })%>
                    </td>
                </tr>                
                <%--Timestamp--%>          
                <tr class="Timestamp">
                    <th scope="row">
                        <label>Timestamp (dd.MM.yyyy hh:mm:ss)</label>
                    </th>
                    <td class="filter">
                        <%=Html.TextBox("Timestamp", Model.TimestampFrom, new { @class = "text", @size = "42", id="Timestamp_From" })%>
                    </td>
                    <td class="filter">
                        <%=Html.TextBox("Timestamp", Model.TimestampTo, new { @class = "text", @size = "42", id="Timestamp_To" })%>
                    </td>
                </tr>
                <tr>
                <td colspan="2">
                <div style="margin: 15px 0px ">
                    <span class="text">Show maximum </span>
                    <%=Html.DropDownList("Limit", Model.GetLimitList(Model.Limit), new { @class = "ddl" })%>                    
                    <input type="submit" value="Search" />
                    <input type="button" value="Clear filters" id="clearFilters"/>
                </div>                
                </td>
                </tr>                
            </tbody>            
        </table>        
    </fieldset>
    <div>
    <table class="grid">
    <thead>
        <tr class="gridResultHeader">
            <th>Id#</th>
            <th>Timestamp</th>
            <th>Level</th>
            <th>Message</th>
        </tr>
    </thead>
    <tbody>
        <%foreach (var log in Model.Logs){%>
        <tr class="gridRow_<%=Html.Encode(log.Level) %>">
            <td><%=log.Id %></td>
            <td><%=log.Date %></td>
            <td><%=log.Level %></td>
            <td><label title="<%=Html.Encode(log.Message) %>" id="<%=log.Id %>" class="showMessage"><%=Html.Encode(log.MessageToShow)%>
            </label></td>
        </tr>
        <%}%>
    </tbody>
    </table>
    </div>

<div id="dialog" title="Message (press ESC to close)">
    <label id="messageText"></label>
</div>    

<script type="text/javascript">
    $(document).ready(function() {

        $("#clearFilters").click(function() {
            // empty all text boxes
            $(".text").val('');

            // set all drop down lists to default value (value=0)
            $(".ddl").val("0");
        });

        $("#filterPanelTitle").click(function() {
            $("#filterPanelTable").slideToggle(100);
        });

        $(".showMessage").click(function() {
            var id = this.id;
            var title = $("#" + id).attr('title');
            $("#messageText").text(title);
            $('#dialog').dialog('open');
        });

        $("#dialog").dialog({
            bgiframe: true,
            autoOpen: false,
            width: 500,
            height: 400,
            modal: true,
            buttons: {
                Close: function() {
                    $(this).dialog('close');
                }
            },
        });
        
        $(function() {
            $('#Timestamp_From').datepicker({
                duration: '',
                showTime: true,
                constrainInput: false,
                stepMinutes: 1,
                stepHours: 1,
                altTimeField: '',
                dateFormat: 'dd.mm.yy',
                time24h: true
            });
            $('#Timestamp_To').datepicker({
                duration: '',
                showTime: true,
                constrainInput: false,
                stepMinutes: 1,
                stepHours: 1,
                altTimeField: '',
                dateFormat: 'dd.mm.yy',
                time24h: true
            });
        });

    });
</script>    
    
</body>
</html>
