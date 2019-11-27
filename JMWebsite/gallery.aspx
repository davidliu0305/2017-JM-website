<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gallery.aspx.cs" Inherits="JMWebsite.gallery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="path/to/jquery.jpop.min.css" rel="stylesheet"/>
    <script src="//code.jquery.com/jquery-1.11.2.min.js"></script>
    <script src="path/to/jquery.jpop.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <a class="popup" href="images/IMG_0831.JPG">
  <img src="images/IMG_0831.JPG""/>
</a>
<a class="popup" href="images/IMG_0832.JPG">
  <img src="images/IMG_0832.JPG"/>
</a>
<a class="popup" href="images/IMG_0833.JPG">
  <img src="images/IMG_0833.JPG"/>
</a>
<%--$('.popup').jPop({
  type: "img",
  gallery: true

});--%>
<%--  .jPop({
  type: "image",
  gallery: true,
  onClick: '',
  onClose: ''
    http://www.jqueryscript.net/lightbox/jQuery-Plugin-For-Multi-purpose-Modal-Popups-jPop.html
    http://www.jqueryscript.net/lightbox/Multifunction-Customizable-Modal-Plugin-For-jQuery-ssi-modal.html
});--%>

    </div>
    </form>
</body>
</html>
