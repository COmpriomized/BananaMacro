/**
 * @typedef {Object} InferenceInput
 * @property {string} text - The input text to analyze
 * @property {string} [context] - Optional context or metadata

/**
 * @typedef {Object} InferenceResult
 * @property {string} label - Predicted label or category
 * @property {number} confidence - Confidence score (0.0 - 1.0)
 * @property {any} [raw] - Optional raw model output
 */

/**
 * Example input shape
 * @type {InferenceInput}
 */
export const exampleInput = {
  text: "Should I trigger macro?",
  context: "GameState:Combat"
};

/**
 * Example output shape
 * @type {InferenceResult}
 */
export const exampleResult = {
  label: "macro_trigger",
  confidence: 0.92,
  raw: { logits: [1.2, -0.3] }
};