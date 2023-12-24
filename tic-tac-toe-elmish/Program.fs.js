import { toString, Record, Union } from "./fable_modules/fable-library.4.9.0/Types.js";
import { int32_type, record_type, list_type, array_type, union_type } from "./fable_modules/fable-library.4.9.0/Reflection.js";
import { copy, fill } from "./fable_modules/fable-library.4.9.0/Array.js";
import { ofArray, cons, head, singleton } from "./fable_modules/fable-library.4.9.0/List.js";
import { createElement } from "react";
import { createObj } from "./fable_modules/fable-library.4.9.0/Util.js";
import { Interop_reactApi } from "./fable_modules/Feliz.2.7.0/./Interop.fs.js";
import { ProgramModule_mkSimple, ProgramModule_run } from "./fable_modules/Fable.Elmish.4.0.0/program.fs.js";
import { Program_withReactBatched } from "./fable_modules/Fable.Elmish.React.4.0.0/react.fs.js";
import "./styles.css";


export class Move extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["X", "O", "Empty"];
    }
}

export function Move_$reflection() {
    return union_type("Program.Move", [], Move, () => [[], [], []]);
}

export class State extends Record {
    constructor(History) {
        super();
        this.History = History;
    }
}

export function State_$reflection() {
    return record_type("Program.State", [], State, () => [["History", list_type(array_type(Move_$reflection()))]]);
}

export class Msg extends Union {
    constructor(Item1, Item2) {
        super();
        this.tag = 0;
        this.fields = [Item1, Item2];
    }
    cases() {
        return ["NextMove"];
    }
}

export function Msg_$reflection() {
    return union_type("Program.Msg", [], Msg, () => [[["Item1", Move_$reflection()], ["Item2", int32_type]]]);
}

export function init() {
    return new State(singleton(fill(new Array(9), 0, 9, new Move(2, []))));
}

export function update(msg, state) {
    const m = msg.fields[0];
    const i = msg.fields[1] | 0;
    const current = copy(head(state.History));
    current[i] = m;
    return new State(cons(current, state.History));
}

export function render(state, dispatch) {
    let elems_8, elems_3, elems, elems_1, elems_2, elems_7, children, elems_4, elems_5, elems_6;
    const squares = (n) => {
        let get$;
        const array = head(state.History);
        get$ = ((index) => array[index]);
        const matchValue = get$(n);
        switch (matchValue.tag) {
            case 1:
                return toString(new Move(1, []));
            case 2:
                return "";
            default:
                return toString(new Move(0, []));
        }
    };
    return createElement("div", createObj(ofArray([["className", "game"], (elems_8 = [createElement("div", createObj(ofArray([["className", "game-board"], (elems_3 = [createElement("div", {
        className: "status",
        children: "Next player: X",
    }), createElement("div", createObj(ofArray([["className", "board-row"], (elems = [createElement("button", {
        className: "square",
        children: squares(0),
        onClick: (_arg) => {
            dispatch(new Msg(new Move(0, []), 0));
        },
    }), createElement("button", {
        className: "square",
        children: squares(1),
        onClick: (_arg_1) => {
            dispatch(new Msg(new Move(0, []), 1));
        },
    }), createElement("button", {
        className: "square",
        children: squares(2),
        onClick: (_arg_2) => {
            dispatch(new Msg(new Move(0, []), 2));
        },
    })], ["children", Interop_reactApi.Children.toArray(Array.from(elems))])]))), createElement("div", createObj(ofArray([["className", "board-row"], (elems_1 = [createElement("button", {
        className: "square",
        children: "",
    }), createElement("button", {
        className: "square",
        children: "",
    }), createElement("button", {
        className: "square",
        children: "",
    })], ["children", Interop_reactApi.Children.toArray(Array.from(elems_1))])]))), createElement("div", createObj(ofArray([["className", "board-row"], (elems_2 = [createElement("button", {
        className: "square",
        children: "",
    }), createElement("button", {
        className: "square",
        children: "",
    }), createElement("button", {
        className: "square",
        children: "",
    })], ["children", Interop_reactApi.Children.toArray(Array.from(elems_2))])])))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_3))])]))), createElement("div", createObj(ofArray([["className", "game-info"], (elems_7 = [(children = ofArray([createElement("li", createObj(singleton((elems_4 = [createElement("button", {
        children: "Go to game start",
    })], ["children", Interop_reactApi.Children.toArray(Array.from(elems_4))])))), createElement("li", createObj(singleton((elems_5 = [createElement("button", {
        children: "Go to move #1",
    })], ["children", Interop_reactApi.Children.toArray(Array.from(elems_5))])))), createElement("li", createObj(singleton((elems_6 = [createElement("button", {
        children: "Go to move #2",
    })], ["children", Interop_reactApi.Children.toArray(Array.from(elems_6))]))))]), createElement("ol", {
        children: Interop_reactApi.Children.toArray(Array.from(children)),
    }))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_7))])])))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_8))])])));
}

ProgramModule_run(Program_withReactBatched("root", ProgramModule_mkSimple(init, update, render)));

//# sourceMappingURL=Program.fs.js.map
