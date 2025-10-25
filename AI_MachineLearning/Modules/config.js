// AI_MachineLearning/modules/config.js

export function getModelConfig() {
  return {
    modelPath: './models/latest/model.onnx',
    inputShape: [1, 512],
    outputSpec: ['label', 'confidence']
  };
}