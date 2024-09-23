$(function(){
    
    var tasks = function(){
        
        $("#add_new_task").on("click",function(){
            var nt = $("#new_task").val();
            if(nt != ''){
                
                var task = '<div class="task-item task-primary">'
                                +'<div class="task-text">'+nt+'</div>'
                                +'<div class="task-footer">'
                                    +'<div class="pull-left"><span class="fa fa-clock-o"></span> now</div>'
                                +'</div>'
                            +'</div>';
                    
                $("#tasks").prepend(task);
            }            
        });
        
        $("#tasks,#tasks_progreess,#tasks_completed").sortable({
            items: "> .task-item",
            connectWith: "#tasks_progreess,#tasks_completed",
            handle: ".task-text",
            cancel: "#tasks_progreess > .task-item, #tasks_completed > .task-item",
            receive: function(event, ui) {
                if(this.id == "tasks_completed"){
                    ui.item.removeClass("task-info").addClass("task-danger");
                }
                if(this.id == "tasks_progreess"){
                    ui.item.removeClass("task-info").addClass("task-success");
                }                
                page_content_onresize();
            }
        }).disableSelection();
        
    }();
    
});
