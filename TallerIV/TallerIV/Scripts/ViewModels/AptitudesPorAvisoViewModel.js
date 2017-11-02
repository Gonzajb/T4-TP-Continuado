var prioridades = [{
    name: 'Excluyente',
    id: 0
}, {
    name: 'Normal',
    id: 1
}, {
    name: 'Baja',
    id: 2
}];

var AptitudPorAviso = function () {
    var self = this;
    self.aptitud_id = ko.observable();
    self.aptitud_nombre = ko.observable();
    self.prioridad = ko.observable();

    self.prioridades = prioridades.map(function (element) {
        // JQuery.UI.AutoComplete expects label & value properties, but we can add our own
        return {
            label: element.name,
            value: element.id,
            // This way we still have acess to the original object
            object: element
        };
    });
    self.prioridadSeleccionada = ko.observable();
}

var AptitudesPorAviso = function () {
    var self = this;
    self.aptitudes = ko.observableArray([new AptitudPorAviso()]);
    self.selectedOption = ko.observable('');
    self.add = function () {
        self.aptitudes.push(new AptitudPorAviso());
    }
    self.remove = function (aptitud) {
        self.aptitudes.remove(aptitud);
    }
}

AptitudesPorAviso.prototype.load = function (aptitudesPorAviso) {

}

AptitudPorAviso.prototype.load = function (aptitudPorAviso) {

}

ko.bindingHandlers.autoComplete = {
    // Only using init event because the Jquery.UI.AutoComplete widget will take care of the update callbacks
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        // { selected: mySelectedOptionObservable, options: myArrayOfLabelValuePairs }
        var settings = valueAccessor();

        var selectedOption = settings.selected;
        var options = settings.options;

        var updateElementValueWithLabel = function (event, ui) {
            // Stop the default behavior
            event.preventDefault();

            // Update the value of the html element with the label 
            // of the activated option in the list (ui.item)
            $(element).val(ui.item.label);

            // Update our SelectedOption observable
            if (typeof ui.item !== "undefined") {
                // ui.item - label|value|...
                selectedOption(ui.item);
            }
        };

        $(element).autocomplete({
            source: options,
            select: function (event, ui) {
                updateElementValueWithLabel(event, ui);
            },
            focus: function (event, ui) {
                updateElementValueWithLabel(event, ui);
            },
            change: function (event, ui) {
                updateElementValueWithLabel(event, ui);
            }
        });
    }
};