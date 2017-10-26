var AptitudPorAviso = function () {
    var self = this;
    self.aptitud_id = ko.observable();
    self.aptitud_nombre = ko.observable();
    self.prioridad = ko.observable();
}

var AptitudesPorAviso = function () {
    var self = this;
    self.aptitudes = ko.observableArray([]);

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