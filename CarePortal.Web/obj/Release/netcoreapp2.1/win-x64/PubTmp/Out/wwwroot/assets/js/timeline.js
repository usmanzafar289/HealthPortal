jQuery(document).ready(function ($)
{
    var $timeline_block = $('.timeline-block');
    $timeline_block.each(function ()
    {
        if ($(this).offset().top > $(window).scrollTop() + $(window).height() * 0.75)
        {
            $(this).find('.timeline-point, .timeline-content').addClass('is-hidden');
        }
    });
    $(window).on('scroll', function ()
    {
        $timeline_block.each(function ()
        {
            if ($(this).offset().top <= $(window).scrollTop() + $(window).height() * 0.75 && $(this).find('.timeline-point').hasClass('is-hidden'))
            { 
                $(this).find('.timeline-point, .timeline-content').removeClass('is-hidden').addClass('bounce-in');
            }
        });
    });
});