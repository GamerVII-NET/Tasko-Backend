! function (e) {
    function t(t) {
        for (var r, i, c = t[0], l = t[1], u = t[2], f = 0, d = []; f < c.length; f++) i = c[f], Object.prototype.hasOwnProperty.call(o, i) && o[i] && d.push(o[i][0]), o[i] = 0;
        for (r in l) Object.prototype.hasOwnProperty.call(l, r) && (e[r] = l[r]);
        for (s && s(t); d.length;) d.shift()();
        return a.push.apply(a, u || []), n()
    }

    function n() {
        for (var e, t = 0; t < a.length; t++) {
            for (var n = a[t], r = !0, c = 1; c < n.length; c++) {
                var l = n[c];
                0 !== o[l] && (r = !1)
            }
            r && (a.splice(t--, 1), e = i(i.s = n[0]))
        }
        return e
    }
    var r = {},
        o = {
            0: 0
        },
        a = [];

    function i(t) {
        if (r[t]) return r[t].exports;
        var n = r[t] = {
            i: t,
            l: !1,
            exports: {}
        };
        return e[t].call(n.exports, n, n.exports, i), n.l = !0, n.exports
    }
    i.m = e, i.c = r, i.d = function (e, t, n) {
        i.o(e, t) || Object.defineProperty(e, t, {
            enumerable: !0,
            get: n
        })
    }, i.r = function (e) {
        "undefined" != typeof Symbol && Symbol.toStringTag && Object.defineProperty(e, Symbol.toStringTag, {
            value: "Module"
        }), Object.defineProperty(e, "__esModule", {
            value: !0
        })
    }, i.t = function (e, t) {
        if (1 & t && (e = i(e)), 8 & t) return e;
        if (4 & t && "object" == typeof e && e && e.__esModule) return e;
        var n = Object.create(null);
        if (i.r(n), Object.defineProperty(n, "default", {
            enumerable: !0,
            value: e
        }), 2 & t && "string" != typeof e)
            for (var r in e) i.d(n, r, function (t) {
                return e[t]
            }.bind(null, r));
        return n
    }, i.n = function (e) {
        var t = e && e.__esModule ? function () {
            return e.default
        } : function () {
            return e
        };
        return i.d(t, "a", t), t
    }, i.o = function (e, t) {
        return Object.prototype.hasOwnProperty.call(e, t)
    }, i.p = "/";
    var c = window.webpackJsonp = window.webpackJsonp || [],
        l = c.push.bind(c);
    c.push = t, c = c.slice();
    for (var u = 0; u < c.length; u++) t(c[u]);
    var s = l;
    a.push([0, 1]), n()
}([function (e, t, n) {
    "use strict";
    n.r(t);
    n(1), n(2)
}, function (e, t) {
    document.addEventListener("DOMContentLoaded", (function (e) {
        for (var t = document.getElementsByClassName("task-block"), n = document.getElementsByClassName("task-dialog-wrapper")[0], r = document.getElementsByClassName("task-dialog-close")[0], o = 0; o < t.length; o++) t[o].addEventListener("click", (function () {
            n.classList.add("active")
        }), !1);
        r.addEventListener("click", (function () {
            n.classList.remove("active")
        }), !1), document.onkeydown = function (e) {
            27 == (e = e || window.event).keyCode && n.classList.remove("active")
        }
    }))
}, function (e, t, n) {
    var r = n(3);
    "string" == typeof r && (r = [
        [e.i, r, ""]
    ]);
    var o = {
        insert: "head",
        singleton: !1
    };
    n(4)(r, o);
    r.locals && (e.exports = r.locals)
}, function (e, t, n) { }]);