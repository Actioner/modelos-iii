var Item = function (data) {
    var item = {
        Id: data.Id,
        Label: data.Label,
        Quantity: data.Quantity,
        Size: data.Size
    };

    return item;
};

var NewItem = function (prefix) {
    var _prefix = prefix;
    var selector = '#' + _prefix + "_";

    var _clear = function () {
        var emptyItem = {
            Id: 0,
            Label: '',
            Quantity: 0,
            Size: 0
        };
        _load(emptyItem);
    };

    var _load = function (item) {
        $(selector + "Id").val(item.Id);
        $(selector + "Label").val(item.Label);
        $(selector + "Quantity").val(item.Quantity);
        $(selector + "Size").val(item.Size);
    };

    var _value = function (name) {
        return $(selector + name).val();
    };

    var newItem = {
        Id: function () { return _value("Id"); },
        Label: function () { return _value("Label"); },
        Quantity: function () { return _value("Quantity"); },
        Size: function () { return _value("Size"); },
        clear: _clear,
        load: _load,
        current: function () {
            return {
                Id: newItem.Id(),
                Label: newItem.Label(),
                Quantity: newItem.Quantity(),
                Size: newItem.Size()
            };
        }
    };

    return newItem;
};

var ItemsManager = function (options) {
    var itemManager;
    var _prefix = options.prefix;
    var _newItem = new NewItem(_prefix);
    var _data = options.model;
    var _items = [];

    if (_data != null) {
        for (var i = 0; i < _data.length; i++) {
            _items[i] = new Item(_data[i]);
        }
    }

    var _init = function () {
        $("#items").on('click', '.info a[data-action="delete"]', function (ev) {
            ev.preventDefault();
            var index = $(this).parents('li').data("index");
            itemManager.deleteItem(index, itemManager.alterItemsCallback);
            itemManager.serializeItems();
        });

        $("#items").on('click', '.info a[data-action="edit"]', function (ev) {
            ev.preventDefault();
            var index = $(this).parents('li').data("index");
            itemManager.loadNewItem(index);
            itemManager.serializeItems();
        });

        $("#newItem").on('click', 'a[data-action="reset"]', function (ev) {
            ev.preventDefault();
            itemManager.clearNewItem();
        });

        $("#newItem").on('click', 'button[data-action="save"]', function (ev) {
            ev.preventDefault();
            var data = _newItem.current();
            $.post(options.url, data, function (result) {
                $("#newItem").html(result.html);
                showErrorsStyled();

                if (result.success) {
                    itemManager.addOrUpdateNewItem(itemManager.alterItemsCallback);
                    itemManager.clearNewItem();
                    itemManager.serializeItems();
                }
            }, 'json');
        });

        this.serializeItems();
    };

    var _clearNewItem = function () {
        $("#newItem").data("index", '');
        _newItem.clear();
    };

    var _deleteItem = function (index, callback) {
        _items.splice(index, 1);
        if ($.isFunction(callback)) {
            callback();
        }
    };

    var _addOrUpdateNewItem = function (callback) {
        var _itemData = {
            Id: _newItem.Id(),
            Label: _newItem.Label(),
            Size: _newItem.Size(),
            Quantity: _newItem.Quantity(),
        };
        var index = $("#newItem").data("index");
        if (index === '' || typeof (index) == "undefined") {
            _items.push(new Item(_itemData));
        } else {
            if ($.isFunction(itemManager.updateItemLabelCallback)) {
                itemManager.updateItemLabelCallback(_items[index].Label, _itemData.Label);
            }
            _items[index] = _itemData;
        }

        if ($.isFunction(callback)) {
            callback();
        }
    };

    var _loadNewItem = function (index) {
        var _itemData = _items[index];
        $("#newItem").data("index", index);
        _newItem.load(_itemData);
    };

    var _serializeItems = function () {
        var _root = $('#items');
        var _html = '';
        for (var index = 0; index < _items.length; index++) {
            _html += "<li data-index='" + index + "'>";
            _html += "<div class='info'>";
            _html += "<span class='label'>" + _items[index].Label;
            _html += "<a data-action='edit' href='javascript:void(0)' style='margin-left: 10px'><span class='icon-pencil'></span></a>";
            _html += "<a data-action='delete' href='javascript:void(0)' style='margin-left: 10px'><span class='icon-trash'></span></a>";
            _html += "</span>";
            _html += "</div>";
            _html += "</li>";
        }
        _root.html(_html);
    };

    itemManager = {
        items: _items,
        newItem: _newItem,
        addOrUpdateNewItem: _addOrUpdateNewItem,
        clearNewItem: _clearNewItem,
        deleteItem: _deleteItem,
        loadNewItem: _loadNewItem,
        serializeItems: _serializeItems,
        init: _init,
        alterItemsCallback: null,
        updateItemLabelCallback: null
    };

    return itemManager;
};

var ScenarioManager = function (options) {
    var _model = options.model;

    var itemsManager = new ItemsManager({
        url: options.newItemUrl,
        prefix: options.newItemPrefix,
        model: _model.Items
    });

    var _init = function () {
        itemsManager.init();

        $('#scenarioForm').submit(function () {
            var data = _getCurrentModel();
            var form = $(this);

            $.ajax({
                url: form.attr('action'),
                type: "POST",
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                headers: {
                    'RequestVerificationToken': $("#requestHeaderToken").val()
                },
                success: function (result) {
                    if (result.success) {
                        if (result.isValid) {
                            window.location = result.action;
                        } else {
                            $(".body-container").html(result.html);

                            _init();
                            showErrorsStyled();
                        }
                    } else {
                        alert('Ha ocurrido un error consulte con el administrador');
                    }
                }
            });

            return false;
        });
    };

    var _getCurrentModel = function () {
        _model.Name = $("#Name").val();
        _model.BinSize = $("#BinSize").val();
        _model.Items = itemsManager.items;

        return _model;
    };

    var scenarioManager = {
        init: _init
    };

    return scenarioManager;
};