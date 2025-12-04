pragma solidity ^0.8.30;
// SPDX-License-Identifier: UNLICENSED

contract FooOld {
    address owner;
    bool isActive; 
    address remover; 
    bool isRemoved; 

    function updateContract() public {
        require(msg.sender == owner); 
        isActive = false;
    }

    function removeItem() public {
        require(msg.sender == remover); 
        isRemoved = true; 
    }
}

contract FooReFactored {
    address owner;
    bool isActive; 
    address remover; 
    bool isRemoved; 
    
    // modifier without parameter
    modifier ownerOnly {
        require(msg.sender == owner); 
        _; // this means just execute the rest, only used within modifiers
    }

    // modifier with parameter
    modifier removerOnly(address addr) {
        require(msg.sender == addr); 
        _; 
    }

    // much readable and prevents having duplicate code all around... 
    // first modifier will be called and the rest will follow    
    function updateContract() public ownerOnly {
        isActive = false;
    }

    // modifier with parametrized modifier
    function removeItem() public removerOnly(remover){
        isRemoved = true; 
    }
}   