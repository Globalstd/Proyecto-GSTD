<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GpsWeb.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="ContentLateral" ContentPlaceHolderID="ContenedorLateral" runat="server">
    <ul class="list-group" runat="server" ID="ulMenuLateral">
    </ul>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenedorPrincipal" runat="server">
    <div class="block-area">
    <!--  Recent Postings -->
    <div class="row">
        <div class="col-lg-6">
            <div class="tile">
                <h2 class="tile-title">Comunicados</h2>
                <div class="tile-config dropdown">
                    <a data-toggle="dropdown" href="#" class="tile-menu"></a>
                    <ul class="dropdown-menu animated pull-right text-right">
                        <li><a href="#">Refresh</a></li>
                        <li><a href="#">Settings</a></li>
                    </ul>
                </div>
                
                <div class="listview narrow">
                    <div class="media p-l-5">
                        <div class="pull-left">
                            <img width="40" src="App_Themes/Certification/img/profile-pics/1.jpg" alt="">
                        </div>
                        <div class="media-body">
                            <small class="text-muted">2 Hours ago by Adrien San</small><br/>
                            <a class="t-overflow" href="#">Cras molestie fermentum nibh, ac semper</a>
                           
                        </div>
                    </div>
                    <div class="media p-l-5">
                        <div class="pull-left">
                            <img width="40" src="App_Themes/Certification/img/profile-pics/2.jpg" alt="">
                        </div>
                        <div class="media-body">
                            <small class="text-muted">5 Hours ago by David Villa</small><br/>
                            <a class="t-overflow" href="#">Suspendisse in purus ut nibh placerat</a>
                            
                        </div>
                    </div>
                    <div class="media p-l-5">
                        <div class="pull-left">
                            <img width="40" src="App_Themes/Certification/img/profile-pics/3.jpg" alt="">
                        </div>
                        <div class="media-body">
                            <small class="text-muted">On 15/12/2013 by Mitch bradberry</small><br/>
                            <a class="t-overflow" href="#">Cras pulvinar euismod nunc quis gravida. Suspendisse pharetra</a>
                            
                        </div>
                    </div>
                    <div class="media p-l-5">
                        <div class="pull-left">
                            <img width="40" src="App_Themes/Certification/img/profile-pics/4.jpg" alt="">
                        </div>
                        <div class="media-body">
                            <small class="text-muted">On 14/12/2013 by Mitch bradberry</small><br/>
                            <a class="t-overflow" href="#">Cras pulvinar euismod nunc quis gravida. </a>
                            
                        </div>
                    </div>
                    <div class="media p-l-5">
                        <div class="pull-left">
                            <img width="40" src="App_Themes/Certification/img/profile-pics/5.jpg" alt="">
                        </div>
                        <div class="media-body">
                            <small class="text-muted">On 13/12/2013 by Mitch bradberry</small><br/>
                            <a class="t-overflow" href="#">Integer a eros dapibus, vehicula quam accumsan, tincidunt purus</a>
                            
                        </div>
                    </div>
                    <div class="media p-5 text-center l-100">
                        <a href="#"><small>VIEW ALL</small></a>
                    </div>
                </div>
            </div>
        </div>
        
        <!-- Tasks to do -->
        <div class="col-lg-6">
            <div class="tile">
                <h2 class="tile-title">Tareas Pendientes</h2>
                <div class="tile-config dropdown">
                    <a data-toggle="dropdown" href="#" class="tile-menu"></a>
                    <ul class="dropdown-menu pull-right text-right">
                        <li id="todo-add"><a href="#">Add New</a></li>
                        <li id="todo-refresh"><a href="#">Refresh</a></li>
                        <li id="todo-clear"><a href="#">Clear All</a></li>
                    </ul>
                </div>
                
                <div class="listview todo-list sortable">
                    <div class="media">
                        <div class="checkbox m-0">
                            <label class="t-overflow">
                                <input type="checkbox">
                                Curabitur quis nisi ut nunc gravida suscipis
                            </label>
                        </div>
                    </div>
                    <div class="media">
                        <div class="checkbox m-0">
                            <label class="t-overflow">
                                <input type="checkbox">
                                Suscipit at feugiat dewoo
                            </label>
                        </div>
                        
                    </div>
                    <div class="media">
                        <div class="checkbox m-0">
                            <label class="t-overflow">
                                <input type="checkbox">
                                Gravida wendy lorem ipsum seen
                            </label>
                        </div>
                        
                    </div>
                    <div class="media">
                        <div class="checkbox m-0">
                            <label class="t-overflow">
                                <input type="checkbox">
                                Fedrix quis nisi ut nunc gravida suscipit at feugiat purus
                            </label>
                        </div>
                        
                    </div>
                </div>
                
                <h2 class="tile-title">Tareas Completadas</h2>
                
                <div class="listview todo-list sortable">
                    <div class="media">
                        <div class="checkbox m-0">
                            <label class="t-overflow">
                                <input type="checkbox" checked="checked">
                                Motor susbect win latictals bin the woodat cool
                            </label>
                        </div>
                        
                    </div>
                    <div class="media">
                        <div class="checkbox m-0">
                            <label class="t-overflow">
                                <input type="checkbox" checked="checked">
                                Wendy mitchel susbect win latictals bin the woodat cool
                            </label>
                        </div>
                        
                    </div>
                    <div class="media">
                        <div class="checkbox m-0">
                            <label class="t-overflow">
                                <input type="checkbox" checked="checked">
                                Latictals bin the woodat cool for the win
                            </label>
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
    <!-- Works -->
    <div class="col-md-12">
    <div class="tile">
        <h2 class="tile-title">Progreso de Proyectos</h2>
        <div class="tile-config dropdown">
            <a data-toggle="dropdown" href="#" class="tooltips tile-menu" title="" data-original-title="Options"></a>
            <ul class="dropdown-menu pull-right text-right">
                <li><a href="#">Edit</a></li>
                <li><a href="#">Delete</a></li>
            </ul>
        </div>
        
        <div class="p-10">
            <div class="m-b-10">
                Joomla CMS website for Lexus Inc. - 60%
                <div class="progress progress-striped progress-alt">
                    <div class="progress-bar progress-bar-info" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 60%"></div>
                </div>
            </div>
            
            <div class="m-b-10">
                Lotus Design's AMC updates - 90%
                <div class="progress progress-striped progress-alt">
                    <div class="progress-bar progress-bar-warning" role="progressbar" aria-valuenow="90" aria-valuemin="0" aria-valuemax="100" style="width: 90%"></div>
                </div>
            </div>    
            
            <div class="m-b-10">
                Chrome Extension developement - 33%
                <div class="progress progress-striped progress-alt">
                    <div class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="33" aria-valuemin="0" aria-valuemax="100" style="width: 33%"></div>
                </div>
            </div>
            
            <div class="m-b-10">
                Wireframes for new website - 50%
                <div class="progress progress-striped progress-alt">
                    <div class="progress-bar progress-bar-danger" role="progressbar" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100" style="width: 50%"></div>
                </div>
            </div>
            
            <div>
                Wordpress Website & Plugin - 99%
                <div class="progress progress-striped progress-alt">
                    <div class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="99" aria-valuemin="0" aria-valuemax="100" style="width: 99%"></div>
                </div>
            </div>
        </div>
    </div>
    </div>
    </div>
    </div>
</asp:Content>
