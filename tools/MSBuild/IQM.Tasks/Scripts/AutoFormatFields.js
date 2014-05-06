// make sure the namespace is setup
if (typeof RIMS == 'undefined') {
    RIMS = function() { };
}

RIMS.AutoFormatFields = function() { };

RIMS.AutoFormatFields.SetupPrefix = function(control, prefix) {
    if ($(control).length > 0) {
        $(control).bindIntoStack(0, 'blur', function(e) {
            if ($(this).val() != "" && prefix != "" && !$(this).val().startsWith(prefix)) {
                $(this).val(prefix + $(this).val());
            }
            e.stopImmediatePropagation();
            return false;
        });
    }
};

RIMS.AutoFormatFields.SetupTime = function(control) {
    if ($(control).length > 0) {
        $(control).blur(function() {
            if ($(this).val().length == 3) {
                var time = "0" + $(this).val().substring(0, 1) + ":" + $(this).val().substring(1, 3)
                $(this).val(time);
            }
            else if ($(this).val().length == 4) {
                var time = $(this).val().substring(0, 2) + ":" + $(this).val().substring(2, 4)
                $(this).val(time);
            }
        });
    }
};