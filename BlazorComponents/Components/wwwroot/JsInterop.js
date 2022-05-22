// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.

export function showPrompt(message) {
  return prompt(message, 'Type anything here');
}

export function CopyText (text) {
    navigator.clipboard.writeText(text).then(function () {
        
    })
        .catch(function (error) {
            console.log(error);
            
        });
    return true;
}