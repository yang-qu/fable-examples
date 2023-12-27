import { toString, Record, Union } from "./fable_modules/fable-library.4.9.0/Types.js";
import { int32_type, record_type, bool_type, list_type, array_type, union_type } from "./fable_modules/fable-library.4.9.0/Reflection.js";
import { copy, fill } from "./fable_modules/fable-library.4.9.0/Array.js";
import { ofArray, length, cons, head, singleton } from "./fable_modules/fable-library.4.9.0/List.js";
import { printf, toText } from "./fable_modules/fable-library.4.9.0/String.js";
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
    constructor(History, XIsNext) {
        super();
        this.History = History;
        this.XIsNext = XIsNext;
    }
}

export function State_$reflection() {
    return record_type("Program.State", [], State, () => [["History", list_type(array_type(Move_$reflection()))], ["XIsNext", bool_type]]);
}

export class Msg extends Union {
    constructor(position) {
        super();
        this.tag = 0;
        this.fields = [position];
    }
    cases() {
        return ["Play"];
    }
}

export function Msg_$reflection() {
    return union_type("Program.Msg", [], Msg, () => [[["position", int32_type]]]);
}

export function init() {
    return new State(singleton(fill(new Array(9), 0, 9, new Move(2, []))), true);
}

export function update(msg, state) {
    const i = msg.fields[0] | 0;
    const current = copy(head(state.History));
    current[i] = (state.XIsNext ? (new Move(0, [])) : (new Move(1, [])));
    return new State(cons(current, state.History), (length(state.History) % 2) === 0);
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
    const nextPlayer = (xIsNext) => {
        const m = state.XIsNext ? (new Move(0, [])) : (new Move(1, []));
        const arg = toString(m);
        return toText(printf("Next player: %s"))(arg);
    };
    return createElement("div", createObj(ofArray([["className", "game"], (elems_8 = [createElement("div", createObj(ofArray([["className", "game-board"], (elems_3 = [createElement("div", {
        className: "status",
        children: nextPlayer(state.XIsNext),
    }), createElement("div", createObj(ofArray([["className", "board-row"], (elems = [createElement("button", {
        className: "square",
        children: squares(0),
        onClick: (_arg) => {
            dispatch(new Msg(0));
        },
    }), createElement("button", {
        className: "square",
        children: squares(1),
        onClick: (_arg_1) => {
            dispatch(new Msg(1));
        },
    }), createElement("button", {
        className: "square",
        children: squares(2),
        onClick: (_arg_2) => {
            dispatch(new Msg(2));
        },
    })], ["children", Interop_reactApi.Children.toArray(Array.from(elems))])]))), createElement("div", createObj(ofArray([["className", "board-row"], (elems_1 = [createElement("button", {
        className: "square",
        children: squares(3),
        onClick: (_arg_3) => {
            dispatch(new Msg(3));
        },
    }), createElement("button", {
        className: "square",
        children: squares(4),
        onClick: (_arg_4) => {
            dispatch(new Msg(4));
        },
    }), createElement("button", {
        className: "square",
        children: squares(5),
        onClick: (_arg_5) => {
            dispatch(new Msg(5));
        },
    })], ["children", Interop_reactApi.Children.toArray(Array.from(elems_1))])]))), createElement("div", createObj(ofArray([["className", "board-row"], (elems_2 = [createElement("button", {
        className: "square",
        children: squares(6),
        onClick: (_arg_6) => {
            dispatch(new Msg(6));
        },
    }), createElement("button", {
        className: "square",
        children: squares(7),
        onClick: (_arg_7) => {
            dispatch(new Msg(7));
        },
    }), createElement("button", {
        className: "square",
        children: squares(8),
        onClick: (_arg_8) => {
            dispatch(new Msg(8));
        },
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
