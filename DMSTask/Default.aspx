<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="DMSTask._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>DMS Tasks</title>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <style>
        boyd {
            padding-top: -10px !important;
        }

        .jumbotron {
            padding: 0px 30px;
        }

        .jumbotron  p {
            padding: 10px;
        }
    </style>
</head>
<body>
    <div class="jumbotron">
      <h1 class="display-4">DMS Tasks!</h1>
      <p class="lead">This is a simple hero unit, a simple jumbotron-style component for calling extra attention to featured content or information.</p>
      <hr class="my-4">
      <p>It uses utility classes for typography and spacing to space content out within the larger container.</p>
      <p class="lead">
        <a class="btn btn-primary btn-lg" href="Pages/Authentication" role="button">Iniciar sesión</a>
        <a class="btn btn-primary btn-lg" href="Pages/Register" role="button">Registrate</a>
      </p>
    </div>
</body>
</html>
