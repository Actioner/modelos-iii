(function(a){a.alerts={verticalOffset:-75,horizontalOffset:0,repositionOnResize:true,overlayOpacity:0.01,overlayColor:"#FFF",draggable:true,okButton:"&nbsp;OK&nbsp;",cancelButton:"&nbsp;Cancel&nbsp;",dialogClass:null,alert:function(b,c,d){if(c==null){c="Alert"}a.alerts._show(c,b,null,"alert",function(e){if(d){d(e)}})},confirm:function(b,c,d){if(c==null){c="Confirm"}a.alerts._show(c,b,null,"confirm",function(e){if(d){d(e)}})},prompt:function(b,c,d,e){if(d==null){d="Prompt"}a.alerts._show(d,b,c,"prompt",function(f){if(e){e(f)}})},_show:function(g,f,c,b,i){a.alerts._hide();a.alerts._overlay("show");a("BODY").append('<div id="popup_container"><h1 id="popup_title"></h1><div id="popup_content"><div id="popup_message"></div></div></div>');if(a.alerts.dialogClass){a("#popup_container").addClass(a.alerts.dialogClass)}var h=(a.browser.msie&&parseInt(a.browser.version)<=6)?"absolute":"fixed";a("#popup_container").css({position:h,zIndex:99999,padding:0,margin:0});a("#popup_title").text(g);a("#popup_content").addClass(b);a("#popup_message").text(f);a("#popup_message").html(a("#popup_message").text().replace(/\n/g,"<br />"));a("#popup_container").css({minWidth:a("#popup_container").outerWidth(),maxWidth:a("#popup_container").outerWidth()});a.alerts._reposition();a.alerts._maintainPosition(true);switch(b){case"alert":a("#popup_message").after('<div id="popup_panel"><input type="button" value="'+a.alerts.okButton+'" id="popup_ok" /></div>');a("#popup_ok").click(function(){a.alerts._hide();i(true)});a("#popup_ok").focus().keypress(function(j){if(j.keyCode==13||j.keyCode==27){a("#popup_ok").trigger("click")}});break;case"confirm":a("#popup_message").after('<div id="popup_panel"><input type="button" value="'+a.alerts.okButton+'" id="popup_ok" /> <input type="button" value="'+a.alerts.cancelButton+'" id="popup_cancel" /></div>');a("#popup_ok").click(function(){a.alerts._hide();if(i){i(true)}});a("#popup_cancel").click(function(){a.alerts._hide();if(i){i(false)}});a("#popup_ok").focus();a("#popup_ok, #popup_cancel").keypress(function(j){if(j.keyCode==13){a("#popup_ok").trigger("click")}if(j.keyCode==27){a("#popup_cancel").trigger("click")}});break;case"prompt":a("#popup_message").append('<br /><input type="text" size="30" id="popup_prompt" />').after('<div id="popup_panel"><input type="button" value="'+a.alerts.okButton+'" id="popup_ok" /> <input type="button" value="'+a.alerts.cancelButton+'" id="popup_cancel" /></div>');a("#popup_prompt").width(a("#popup_message").width());a("#popup_ok").click(function(){var e=a("#popup_prompt").val();a.alerts._hide();if(i){i(e)}});a("#popup_cancel").click(function(){a.alerts._hide();if(i){i(null)}});a("#popup_prompt, #popup_ok, #popup_cancel").keypress(function(j){if(j.keyCode==13){a("#popup_ok").trigger("click")}if(j.keyCode==27){a("#popup_cancel").trigger("click")}});if(c){a("#popup_prompt").val(c)}a("#popup_prompt").focus().select();break}if(a.alerts.draggable){try{a("#popup_container").draggable({handle:a("#popup_title")});a("#popup_title").css({cursor:"move"})}catch(d){}}},_hide:function(){a("#popup_container").remove();a.alerts._overlay("hide");a.alerts._maintainPosition(false)},_overlay:function(b){switch(b){case"show":a.alerts._overlay("hide");a("BODY").append('<div id="popup_overlay"></div>');a("#popup_overlay").css({position:"absolute",zIndex:99998,top:"0px",left:"0px",width:"100%",height:a(document).height(),background:a.alerts.overlayColor,opacity:a.alerts.overlayOpacity});break;case"hide":a("#popup_overlay").remove();break}},_reposition:function(){var c=((a(window).height()/2)-(a("#popup_container").outerHeight()/2))+a.alerts.verticalOffset;var b=((a(window).width()/2)-(a("#popup_container").outerWidth()/2))+a.alerts.horizontalOffset;if(c<0){c=0}if(b<0){b=0}if(a.browser.msie&&parseInt(a.browser.version)<=6){c=c+a(window).scrollTop()}a("#popup_container").css({top:c+"px",left:b+"px"});a("#popup_overlay").height(a(document).height())},_maintainPosition:function(b){if(a.alerts.repositionOnResize){switch(b){case true:a(window).bind("resize",a.alerts._reposition);break;case false:a(window).unbind("resize",a.alerts._reposition);break}}}};jAlert=function(b,c,d){a.alerts.alert(b,c,d)};jConfirm=function(b,c,d){a.alerts.confirm(b,c,d)};jPrompt=function(b,c,d,e){a.alerts.prompt(b,c,d,e)}})(jQuery);