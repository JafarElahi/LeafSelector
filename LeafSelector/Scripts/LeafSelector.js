var LeafSelector = {
    initialize: function(){
        $('.leafselector').each(function (index, leaferElement) {
            var leafer = {
                element: leaferElement,
                input: null,
                selector: null,
                panel: null,
                name: $(leaferElement).attr('data-name'),
                source: $(leaferElement).attr('data-source'),
                templateFunction: null,
                seperator: $(leaferElement).hasAttr('data-seperator') ? $(leaferElement).attr('data-seperator') : " <",
                setData: function (data) {
                    data.seperator = leafer.seperator;
                    data.rootText = leafer.rootText;
                    leafer.panel.html(leafer.templateFunction(data));
                    leafer.panel.find('span.parent').click(function () {
                        var id = $(this).attr("data-parentid");
                        leafer.fetch(id);
                    });
                    leafer.panel.find('.close').click(function () {
                        leafer.toggle();
                    });
                    leafer.panel.find('li.sub[data-isleaf="false"]').click(function () {
                        var id = $(this).attr("data-id");
                        leafer.fetch(id);
                    });
                    leafer.panel.find('li.sub[data-isleaf="true"]').click(function () {
                        leafer.input.val($(this).attr("data-id"));
                        leafer.selector.find('.text').text($(this).text());
                        leafer.toggle();
                    });
                },
                fetch: function (id) {
                    if (id in leafer) {
                        leafer.setData(leafer[id]);
                        event.stopPropagation();
                    } else {
                        $.ajax({
                            data: { id: id },
                            url: this.source,
                            success: function(data){
                                leafer[id] = data;   
                                leafer.setData(data);
                            }
                        });
                    }
                },
                toggle: function() {
                    leafer.panel.width(leafer.selector.width() + 50);
                    leafer.panel.toggle();
                }
            };
            var scriptname = "#" + $(leaferElement).attr('id') + "-template";
            if ($(scriptname).length > 0) {
                leafer.templateFunction = doT.template($(scriptname).html());
            } else {
                leafer.templateFunction = doT.template(' \
<div class="parents"> \
{{?it.Parents != null}} \
{{~it.Parents :parent:index}} \
    {{?index>0}}<span>{{=it.seperator}}{{?}}</span><span class="parent" data-parentid="{{=parent.Id}}">{{=parent.Text}}</span> \
{{~}} \
{{?}} \
<span class="close">X</span> \
</div> \
<ul> \
{{~it.Items :item:index}} \
    <li class="sub" data-id="{{=item.Id}}" data-isleaf="{{=item.IsLeaf}}">{{=item.Text}}</li> \
{{~}} \
</ul>');
            }
            var options = {
                text: $(leaferElement).hasAttr("data-text") ? $(leaferElement).attr('data-text') : "",
                value: $(leaferElement).attr('data-id'),
                leaferName: leafer.name
            };
            var selectorHtml = ' \
<div class="panel popup" style="display: none"></div> \
<div class="selector btn btn-primary popupOwner"><span class="text">{{=it.text}}</span><span class="caret"></span></div> \
<input type="hidden" name="{{=it.leaferName}}" value="{{=it.value}}" />';
            var bodyTemplate = doT.template(selectorHtml);
            $(leaferElement).append(bodyTemplate(options));
            var parrentId = $(leaferElement).attr("data-parentId");
            leafer.input = $(leaferElement).find('input');
            leafer.selector = $(leaferElement).find('.selector');
            leafer.panel = $(leaferElement).find('.panel');
            leafer.selector.click(function () {
                leafer.toggle();
            });
            leafer.fetch(parrentId);
        });
    }
};
$(document).ready(LeafSelector.initialize);
$('body').click(function (e) {
    var popupOwner = $('.popupOwner');
    if (popupOwner.is(e.target)
        || popupOwner.has(e.target).length != 0
        )
    {
        $('.popup').not($(e.target).closest('.popupOwner').prev('.popup')).hide();
    }
    else {
        $('.popup').not($(e.target).closest('.popup')).hide();
    }
});
$.fn.hasAttr = function (name) {
    var attr = this.attr(name);
    return attr !== undefined && attr != false;
};