(function ($)
{
	$.fn.iframePostForm = function (options)
	{
		var response,
			returnReponse,
			element,
			status = true,
			iframe;
		
		options = $.extend({}, $.fn.iframePostForm.defaults, options);
		
		// console.log("checking whether to add iframe");
		// Add the iframe.
		if (!$('#' + options.iframeID).length)
		{
		    // console.log("adding iframe");
		    $('body').append('<iframe id="' + options.iframeID + '" name="' + options.iframeID + '" style="display:none" />');
		}

		return $(this).each(function ()
		{
			element = $(this);
			
			
			// Target the iframe.
			element.attr('target', options.iframeID);
			
			
			// Submit listener.
			element.submit(function ()
			{
				// If status is false then abort.
				status = options.post.apply(this);
				
				if (status === false)
				{
					return status;
				}
				
				
				iframe = $('#' + options.iframeID).load(function ()
				{
					response = iframe.contents().find('body');

					if (options.json)
					{
						returnReponse = $.parseJSON(response.html());
					}
					
					else
					{
						returnReponse = response.html();
					}
					
					
					options.complete.apply(this, [returnReponse]);
					
					iframe.unbind('load');
					
					
					setTimeout(function ()
					{
						response.html('');
					}, 1);
				});
			});
		});
	};
	
	$.fn.iframePostForm.defaults =
	{
		iframeID : 'iframe-post-form',       // Iframe ID.
		json : true,                         // Parse server response as a json object.
		post : function () {},               // Form onsubmit.
		complete : function (response) {
		    /*console.log(response);
		    locVm.editingLoc().Photos(response);
		    locVm.cancelPhotoUpload();*/
		}    // After response from the server has been received.
	};
})(jQuery);