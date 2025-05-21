<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Processing.ascx.cs" Inherits="Processing" %>
<style type="text/css">   
.mydiv 

{
text-align:center;
background-color: #FFCC00;
border: 1px solid #f00;
filter:alpha(opacity=90);/*IE*/
opacity:0.9;/*FF*/
z-index:999;
width:300px;
height: 100px;
left:50%;/*FF IE7*/
top:50%;/*FF IE7*/
margin-left:-150px!important;/*FF IE7 该值为本身宽的一半 */
margin-top:-60px!important;/*FF IE7 该值为本身高的一半*/
margin-top:0px;
position:fixed!important;/*FF IE7*/
position:absolute;/*IE6*/
_top:       expression(eval(document.compatMode &&
            document.compatMode=='CSS1Compat') ?
            documentElement.scrollTop + (document.documentElement.clientHeight-this.offsetHeight)/2 :/*IE6*/
            document.body.scrollTop + (document.body.clientHeight - this.clientHeight)/2);/*IE5 IE5.5*/

} 

</style>
<div id='doing' class="mydiv">
    <img src="img/loading.gif" alt="" style="margin-top:30px" />
    <table id="Table1">
        <tr align='center' valign='middle'>
            <td>
                <b><span style="color: #ff0066">Loading...</span></b></td>
        </tr>
    </table>
</div>

<%--<script language="javascript" type="text/javascript">
function ShowWaiting()
{
document.getElementById('doing').style.visibility = 'visible';
}
function CloseWaiting()
{
document.getElementById('doing').style.visibility = 'hidden';
}
function MyOnload()
{
document.getElementById('doing').style.visibility = 'hidden';
}
if (window.onload == null)
{
window.onload = MyOnload;
}
</script>
--%>
