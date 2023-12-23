import { Union, Record } from "./fable_modules/fable-library.4.9.0/Types.js";
import { union_type, record_type, int32_type } from "./fable_modules/fable-library.4.9.0/Reflection.js";
import { Cmd_none } from "./fable_modules/Fable.Elmish.4.0.0/cmd.fs.js";
import { createElement } from "react";
import { createObj } from "./fable_modules/fable-library.4.9.0/Util.js";
import { Interop_reactApi } from "./fable_modules/Feliz.2.7.0/./Interop.fs.js";
import { singleton, ofArray } from "./fable_modules/fable-library.4.9.0/List.js";
import { ProgramModule_mkProgram, ProgramModule_run } from "./fable_modules/Fable.Elmish.4.0.0/program.fs.js";
import { Program_withReactBatched } from "./fable_modules/Fable.Elmish.React.4.0.0/react.fs.js";
import "./styles.css";


export class State extends Record {
    constructor(Count) {
        super();
        this.Count = (Count | 0);
    }
}

export function State_$reflection() {
    return record_type("Program.State", [], State, () => [["Count", int32_type]]);
}

export class Msg extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Increment", "Decrement"];
    }
}

export function Msg_$reflection() {
    return union_type("Program.Msg", [], Msg, () => [[], []]);
}

export function init() {
    return [new State(0), Cmd_none()];
}

export function update(msg, state) {
    if (msg.tag === 1) {
        return [new State(state.Count - 1), Cmd_none()];
    }
    else {
        return [new State(state.Count + 1), Cmd_none()];
    }
}

export function render(state, dispatch) {
    let elems_8, elems_3, elems, elems_1, elems_2, elems_7, children, elems_4, elems_5, elems_6;
    return createElement("div", createObj(ofArray([["className", "game"], (elems_8 = [createElement("div", createObj(ofArray([["className", "game-board"], (elems_3 = [createElement("div", {
        className: "status",
        children: "Next player: X",
    }), createElement("div", createObj(ofArray([["className", "board-row"], (elems = [createElement("button", {
        className: "square",
        children: "X",
    }), createElement("button", {
        className: "square",
        children: "X",
    }), createElement("button", {
        className: "square",
        children: "X",
    })], ["children", Interop_reactApi.Children.toArray(Array.from(elems))])]))), createElement("div", createObj(ofArray([["className", "board-row"], (elems_1 = [createElement("button", {
        className: "square",
        children: "X",
    }), createElement("button", {
        className: "square",
        children: "X",
    }), createElement("button", {
        className: "square",
        children: "X",
    })], ["children", Interop_reactApi.Children.toArray(Array.from(elems_1))])]))), createElement("div", createObj(ofArray([["className", "board-row"], (elems_2 = [createElement("button", {
        className: "square",
        children: "X",
    }), createElement("button", {
        className: "square",
        children: "X",
    }), createElement("button", {
        className: "square",
        children: "X",
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

ProgramModule_run(Program_withReactBatched("root", ProgramModule_mkProgram(init, update, render)));

//# sourceMappingURL=Program.fs.js.map
