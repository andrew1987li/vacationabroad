<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DropDownMenu.ascx.cs" Inherits="DropDownMenu" %>
<%@ OutputCache duration="100" varybyparam="none" %>

<script type="text/javascript">
        var menu = function() {
            var t = 4, z = 50, s = 6, a;
            function dd(n) { this.n = n; this.h = []; this.c = [] }
            dd.prototype.init = function(p, c) {
                a = c; var w = document.getElementById(p), s = w.getElementsByTagName('ul'), l = s.length, i = 0;
                li = w.getElementsByTagName('li');
                
                for (i; i < l; i++) {
                    var h = s[i].parentNode;

                    this.h[i] = h; this.c[i] = s[i];
                    h.onmouseover = new Function(this.n + '.st(' + i + ',true)');
                    h.onmouseout = new Function(this.n + '.st(' + i + ')');
                }
            }
            dd.prototype.st = function(x, f) {
                var c = this.c[x], h = this.h[x], p = h.getElementsByTagName('a')[0];
                clearInterval(c.t); c.style.overflow = 'hidden';
                if (f) {
                    p.className += ' ' + a;
                    if (!c.mh) { c.style.display = 'block'; c.style.height = ''; c.mh = c.offsetHeight; c.style.height = 0 }
                    if (c.mh == c.offsetHeight) { c.style.overflow = 'visible' }
                    else { c.style.zIndex = z; z++; c.t = setInterval(function() { sl(c, 1) }, t) }
                } else { p.className = p.className.replace(a, ''); c.t = setInterval(function() { sl(c, -1) }, t) }
            }
            function sl(c, f) {
                var h = c.offsetHeight;
                if ((h <= 0 && f != 1) || (h >= c.mh && f == 1)) {
                    if (f == 1) { c.style.filter = ''; c.style.opacity = 1; c.style.overflow = 'visible' }
                    clearInterval(c.t); return
                }
                var d = (f == 1) ? Math.ceil((c.mh - h) / s) : Math.ceil(h / s), o = h / c.mh;
                c.style.opacity = o; c.style.filter = 'alpha(opacity=' + (o * 100) + ')';
                c.style.height = h + (d * f) + 'px'
            }
            return { dd: dd }
        } ();        
    </script>
    <div id="divInclude" runat="server"></div>
    
    <ul class="menu" id="menu" name="menu">  
    <%--<li style="width:15px;"></li>--%>      
    </ul>       
    
    <script type="text/javascript">
        var vAgain = true;

        InitializeHDropdowns();


        var menu = new menu.dd("menu");
        menu.init("menu", "menuhover");



        function InitializeHDropdowns() {
            if (vAgain == true) {
                var i;
                var region = document.getElementsByTagName("ul").namedItem("menu");

                for (i = 0; i < regionids.length; i++) {
                    var mnuLI = document.createElement("li");
                    var vReg = regionids[i];
                    // create new main menu link and text
                    pickLink = document.createElement('a');
                    var vRegName = "Region";

                    switch (vReg) {
                        case 1:
                            vRegName = "Africa";
                            break;
                        case 2:
                            vRegName = "Asia";
                            break;
                        case 4:
                            vRegName = "Caribbean";
                            break;
                        case 5:
                            vRegName = "C. America";
                            break;
                        case 6:
                            vRegName = "Europe";
                            break;
                        case 7:
                            vRegName = "Middle East";
                            break;
                        case 8:
                            vRegName = "N. America";
                            break;
                        case 3:
                            vRegName = "Oceania";
                            break;
                        case 9:
                            vRegName = "S. America";
                            break;
                        default:
                            vRegName = "No Region";
                    }
                    pickText = document.createTextNode(vRegName);

                    // add the text as a child of the link
                    pickLink.appendChild(pickText);

                    if (vRegName == "N. America")
                        vRegName = "North America";
                    if (vRegName == "S. America")
                        vRegName = "South America";
                    if (vRegName == "C. America")
                        vRegName = "Central America";
                    var vLink = 'http://www.vacations-abroad.com/' + vRegName + '/default.aspx';
                    vLink = vLink.toLowerCase();
                    vLink = vLink.replace(/ /g, "_");
                    // set the href to # and call picker when clicked or tabbed to		
                    pickLink.setAttribute('href', vLink);
                    mnuLI.appendChild(pickLink);

                    
                    

                    pickLink.setAttribute('class', 'menulink');
                    pickLink.setAttribute('className', 'menulink');

                    region.appendChild(mnuLI);
                }
                //Blog
                var mnuLI2 = document.createElement("li");
                var pickLink2 = document.createElement('a');
                // add the text as a child of the link
                pickText2 = document.createTextNode('Blog');
                pickLink2.appendChild(pickText2);

                // set the href to # and call picker when clicked or tabbed to
                pickLink2.setAttribute('href', 'http://vacationsabroad.blogspot.com');
                pickLink2.setAttribute('class', 'menulink');
                pickLink2.setAttribute('className', 'menulink');
                mnuLI2.appendChild(pickLink2);
                region.appendChild(mnuLI2);

                //List your rental
                var mnuLI3 = document.createElement("li");
                var pickLink3 = document.createElement('a');
                // add the text as a child of the link
                pickText3 = document.createTextNode('List Property');
                pickLink3.appendChild(pickText3);

                // set the href to # and call picker when clicked or tabbed to
                pickLink3.setAttribute('href', 'http://www.vacations-abroad.com/applications.htm');
                pickLink3.setAttribute('class', 'menulink');
                pickLink3.setAttribute('className', 'menulink');
                mnuLI3.appendChild(pickLink3);
                region.appendChild(mnuLI3);
            }
            vAgain = false;
        }
    </script>